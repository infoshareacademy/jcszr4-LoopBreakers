using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoopBreakers.WebApp.Contracts;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LoopBreakers.WebApp.Services
{
    public class ClientService : IClientService 
    {
        private readonly IBaseRepository<ApplicationUser> _clientRepository;
        public ClientService(IBaseRepository<ApplicationUser> clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<IEnumerable<ApplicationUser>> FilterBy(SearchViewModel filter)
        {
            var clientQuery = _clientRepository.GetAllQueryable()
                                                                        .Where(n => n.Id > 1);

            if (filter.SearchText != null && filter.SearchText.Length > 2)
            {
                clientQuery = clientQuery.Where(n => n.LastName.Contains(filter.SearchText) ||
                                                    n.FirstName.Contains(filter.SearchText) ||
                                                    n.Email.Contains(filter.SearchText) ||
                                                    n.Address.Contains(filter.SearchText) ||
                                                    n.Phone.Contains(filter.SearchText) ||
                                                    n.Company.Contains(filter.SearchText) ||
                                                    n.Iban.Contains(filter.SearchText));
            }
            return await clientQuery.ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _clientRepository.FindAll();
        }

        public async Task<ApplicationUser> FindTransferPerformer(string userEmail)
        {
            return await _clientRepository.GetAllQueryable().FirstOrDefaultAsync(n => n.Email == userEmail);
        }

        public async Task<ApplicationUser> FindRecipient(string iban)
        {
            return await _clientRepository.GetAllQueryable().FirstOrDefaultAsync(n => n.Iban == iban);
        }

        public async Task<bool> PerformerBalanceUpdateAfterTransfer(ApplicationUser user)
        {
            return await _clientRepository.Update(user);
        }

        public async Task<bool> RecipientBalanceUpdateAfterTransfer(ApplicationUser user)
        {
            return await _clientRepository.Update(user);
        }

        public async Task<ApplicationUser> FindLoggedUser(string email)
        {
            return await _clientRepository.GetAllQueryable().FirstOrDefaultAsync(n => n.Email == email);
        }
    }
}
