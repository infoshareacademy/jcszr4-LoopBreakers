using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<ApplicationUser>> FilterBy(SearchViewModel filter);
        ApplicationUser FindTransferPerformer(string userEmail);
        ApplicationUser FindRecipient (string iban);
        ApplicationUser FindLoggedUser(string email);

        void PerformerBalanceUpdateAfterTransfer (ApplicationUser user);
        void RecipientBalanceUpdateAfterTransfer(ApplicationUser user);

        IEnumerable<ApplicationUser> GetAll();
    }
}
