using LoopBreakers.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Repositories;

namespace LoopBreakers.WebApp.Contracts
{
    public interface ITransfersRepository : IBaseRepository<Transfer>
    {
        Task<IList<Transfer>> FindByDates(TransfersRepository.SerchTransferDTO filter);
    }
}
