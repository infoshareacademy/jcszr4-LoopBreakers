using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoopBreakers.WebApp.Services
{
    public class TransferService : ITransferService
    {
        private readonly IBaseRepository<Transfer> _transfersRepository;
        public TransferService(IBaseRepository<Transfer> transfersRepository)
        {
            _transfersRepository = transfersRepository;
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
            return await transfersQuery.ToListAsync();
        }
        public async Task<bool> CreateNew(Transfer transfer)
        {
            return await _transfersRepository.Create(transfer);
        }
    }
}
