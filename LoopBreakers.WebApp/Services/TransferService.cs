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
        public async Task<IEnumerable<Transfer>> FilterBy(SearchViewModel filter)
        {
            var transfersQuery = _db.Transfers.AsQueryable();

            if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            {
                transfersQuery = transfersQuery.Where(q => q.Created >= filter.DateFrom.Value && q.Created <= filter.DateTo.Value);
            }
            if (filter.SearchText != null && filter.SearchText.Length > 2)
            {
                transfersQuery = transfersQuery.Where(n => n.LastName.StartsWith(filter.SearchText));
            }
            return await transfersQuery.ToListAsync();
        }
        public void CreateNew(Transfer transfer)
        {
             _db.Transfers.Add(transfer);
             _db.SaveChanges();
        }
    }
}
