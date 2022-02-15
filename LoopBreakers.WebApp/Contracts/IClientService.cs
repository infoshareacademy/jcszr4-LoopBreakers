using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<ApplicationUser>> FilterBy(SearchClientViewModel filter);
        ApplicationUser FindTransferPerformer(string userEmail);
        ApplicationUser FindRecipent (string iban);
        ApplicationUser FindLoggedUser(string email);

        void PerformerBalanceUpadateAfterTransfer (ApplicationUser user);
        void RecipentBalanceUpadateAfterTransfer(ApplicationUser user);

        IEnumerable<ApplicationUser> GetAll();
    }
}
