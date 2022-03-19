using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Enums;
using LoopBreakers.ReportModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoopBreakers.WebApp.Services
{
    public class TransferService : ITransferService
    {
        private readonly IBaseRepository<Transfer> _transfersRepository;
        private readonly IClientService _clientService;
        private readonly ReportService _reportService;
        private readonly IMapper _mapper;
        public TransferService(IBaseRepository<Transfer> transfersRepository, IClientService clientService, UserManager<ApplicationUser> userManager, IMapper mapper, ReportService reportService)
        {
            _transfersRepository = transfersRepository;
            _clientService = clientService;
            _mapper = mapper;
            _reportService = reportService;
        }

        public async Task<IEnumerable<Transfer>> FilterBy(SearchViewModel filter, ApplicationUser user)
        {
            var transfersQuery = _transfersRepository.GetAllQueryable();

            if (user.Id > 1)
            {
                transfersQuery = transfersQuery.Where(q => q.FromId == user.Id.ToString() ||
                                                           q.Iban == user.Iban);
            }
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                transfersQuery = transfersQuery.Where(q => q.Created >= filter.DateFrom.Value && q.Created <= filter.DateTo.Value);
            }
            if (filter.SearchText != null && filter.SearchText.Length > 2)
            {
                transfersQuery = transfersQuery.Where(n => n.LastName.Contains(filter.SearchText) ||
                                                           n.FirstName.Contains(filter.SearchText) ||
                                                           n.Reference.Contains(filter.SearchText) ||
                                                           n.Iban.Contains(filter.SearchText));
            }

            var data = await transfersQuery.ToListAsync();
            for(int i = 0; i< data.Count(); i++)
            {
                if (user.Iban == data[i].Iban)
                {
                    data[i].Type = TransferType.Funding;
                }
            }
            return data;
        }
        public async Task<bool> CreateNew(Transfer transfer)
        {
            return await _transfersRepository.Create(transfer);
        }

        public async Task<bool> SendTransfer(TransferPerformDTO transfer, ApplicationUser user)
        {
            if (transfer.Amount > user.Balance)
            {
                return false;
            }
            
            transfer.Created = DateTime.Now;
            transfer.FromId = user.Id.ToString();
            transfer.Type = TransferType.Payment;
            var transferOut = _mapper.Map<Transfer>(transfer);
            var transferReportOut = _mapper.Map<TransferReportDTO>(transfer);
            transferReportOut.CountryCode = transfer.Iban.Substring(0, 2).ToUpper();
            
            await CreateNew(transferOut);
            user.Balance -= transferOut.Amount;
            await _clientService.PerformerBalanceUpdateAfterTransfer(user);

            var transferRecipient = await _clientService.FindRecipient(transfer.Iban);
            if (transferRecipient != null)
            {
                transferRecipient.Balance += transferOut.Amount;
                await _clientService.RecipientBalanceUpdateAfterTransfer(transferRecipient);
            }

            await _reportService.SendTransferReport(transferReportOut);
            await _reportService.SendActivityReport(new ActivityReportDTO
            {
                Description = "Transfer created",
                Created = DateTime.Now,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ActivityType = ActivityEvents.transfering
            });

            return true;
        }
    }
}
