using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface ITransferService
    {
        Task<IEnumerable<Transfer>> FilterBy(SearchTransferViewModel filter);
        void CreateNew(Transfer transfer);
     

    }
}
