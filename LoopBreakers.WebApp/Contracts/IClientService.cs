using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<ApplicationUser>> FilterBy(SearchViewModel filter);
        Task<ApplicationUser> FindTransferPerformer(string userEmail);
        Task<ApplicationUser> FindRecipient (string iban);
        Task<ApplicationUser> FindLoggedUser(string email);
        Task<bool> PerformerBalanceUpdateAfterTransfer (ApplicationUser user);
        Task<bool> RecipientBalanceUpdateAfterTransfer(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAll();
    }
}
