using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Contracts
{
    public interface ITransferService
    {
        Task<IEnumerable<Transfer>> FilterBy(SearchTransferViewModel filter);
    }
}
