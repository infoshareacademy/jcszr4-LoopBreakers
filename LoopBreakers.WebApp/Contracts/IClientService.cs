using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<ApplicationUser>> FilterBy(SearchClientViewModel filter);
    }    
}
