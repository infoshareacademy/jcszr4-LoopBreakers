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
        public RecipientService(IBaseRepository<Recipient> recipientRepository)
        {
            _recipientRepository = recipientRepository;
        }

        public async Task<IEnumerable<Recipient>> FilterBy(SearchViewModel filter, ApplicationUser user)
        {
            var recipientQuery = _recipientRepository.GetAllQueryable()
                                                                        .Where(u=>u.FromId == user.Id.ToString());

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
