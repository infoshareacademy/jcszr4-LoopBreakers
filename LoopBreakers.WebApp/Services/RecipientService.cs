using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Services
{
    public class RecipientService : IRecipientService
    {
        private readonly IBaseRepository<Recipient> _recipientRepository;
        private readonly ApplicationDbContext _db;

        public RecipientService(ApplicationDbContext db,
            IBaseRepository<Recipient> recipientRepository)
        {
            _db = db;
            _recipientRepository = recipientRepository;
        }
        public async Task<IEnumerable<Recipient>> FilterBy(SearchRecipientViewModel filter)
        {
            var recipientQuery = _db.Recipients.AsQueryable();

            if (filter.SearchText != null && filter.SearchText.Length > 2)
            {
                recipientQuery = recipientQuery.Where(n => n.LastName.Contains(filter.SearchText) ||
                                                    n.FirstName.Contains(filter.SearchText) ||
                                                    n.Email.Contains(filter.SearchText) ||
                                                    n.Address.Contains(filter.SearchText) ||
                                                    n.Iban.Contains(filter.SearchText));
            }
            return await recipientQuery.ToListAsync();
        }
    }
}
