using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoopBreakers.WebApp.Services
{
    public class TransferService : ITransferService
    {
        private readonly IBaseRepository<Transfer> _transfersRepository;
        private readonly ApplicationDbContext _db;
        public TransferService(ApplicationDbContext db,
            IBaseRepository<Transfer> transfersRepository)
        {
            _db = db;
            _transfersRepository = transfersRepository;
        }
        public async Task<IEnumerable<Transfer>> FilterBy(SearchTransferViewModel filter)
        {
            var transfersQuery = _db.Transfers.AsQueryable();
            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                transfersQuery = transfersQuery.Where(q =>
                    q.Created >= filter.DateFrom.Value && q.Created <= filter.DateTo.Value);
            }
            else if (filter.Name!=null)
            {
                if (filter.Name.Length > 2) 
                {
                    if (transfersQuery.All(n => n.LastName.Length > filter.Name.Length))
                    {

                        transfersQuery = transfersQuery.Where(n => n.LastName.Substring(0, filter.Name.Length).ToLower() == filter.Name.ToLower());                    }
                    else
                    {
                        transfersQuery = transfersQuery.Where(n => n.LastName.ToLower()==filter.Name.ToLower());

                    }
                }
                else
                {
                    transfersQuery = transfersQuery.Where(n => n.LastName.ToLower() == filter.Name.ToLower());

                }
            }
            else if (filter.Clear != null)
            {
                 transfersQuery = _db.Transfers.AsQueryable();
            }


            return await transfersQuery.ToListAsync();
        }
    }
}
