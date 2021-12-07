using LoopBreakers.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface ITransfersRepository : IBaseRepository<Transfer>
    {
        Task<IList<Transfer>> FindByDates(DateTime dateFrom, DateTime dateTo);
    }
}
