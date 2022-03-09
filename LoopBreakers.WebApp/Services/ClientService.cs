using LoopBreakers.DAL.Context;
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
    public class ClientService : IClientService 
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
        public IEnumerable<ApplicationUser> GetAll()
        {
            return  _db.Users.ToList();
        }
        public ApplicationUser FindTransferPerformer(string userEmail)
        {
            return _db.Users.FirstOrDefault(n => n.Email == userEmail);
        }
        public ApplicationUser FindRecipient(string iban)
        {
            return _db.Users.FirstOrDefault(n => n.Iban == iban);
        }
        public void PerformerBalanceUpdateAfterTransfer(ApplicationUser user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
        public void RecipientBalanceUpdateAfterTransfer(ApplicationUser user)
        {
            if(user != null)
                _db.Users.Update(user);
            _db.SaveChanges();
        }

        public ApplicationUser FindLoggedUser(string email)
        {
            return _db.Users.FirstOrDefault(n => n.Email == email);
        }
    }
}
