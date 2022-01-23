﻿using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoopBreakers.WebApp.Contracts;

namespace LoopBreakers.WebApp.Services
{
    public class ClientService: IClientService
    {
        private readonly IBaseRepository<ApplicationUser> _clientRepository;
        private readonly ApplicationDbContext _db;
        public ClientService(ApplicationDbContext db,
            IBaseRepository<ApplicationUser> applicationUserRepository)
        {
            _db = db;
            _clientRepository = applicationUserRepository;
        }
        public async Task<IEnumerable<ApplicationUser>> FilterBy(SearchClientViewModel filter)
        {  
            var clientQuery = _db.Users.AsQueryable();

            //if (filter.LastName != null && filter.LastName.Length > 2)
            //{
            //    clientQuery = clientQuery.Where(n => n.LastName.StartsWith(filter.LastName));
            //}
            if (filter.SearchText != null && filter.SearchText.Length > 2)
            {
                clientQuery = clientQuery.Where(n => n.LastName.Contains(filter.SearchText) || 
                                                    n.FirstName.Contains(filter.SearchText) ||
                                                    n.Email.Contains(filter.SearchText) ||
                                                    n.Address.Contains(filter.SearchText) ||
                                                    n.Phone.Contains(filter.SearchText)||
                                                    n.Company.Contains(filter.SearchText) ||
                                                    n.Iban.Contains(filter.SearchText));
            }
            return await clientQuery.ToListAsync();
        }
        public ApplicationUser FindTransferPerformer(TransferPerformDTO transfer)
        {
            return _db.Users.Where(n => n.LastName == transfer.UserSurname).FirstOrDefault();
        }
        public void BalanceUpadateAfterTransfer(ApplicationUser user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

    }
}
