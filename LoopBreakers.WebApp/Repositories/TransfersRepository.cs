using LoopBreakers.WebApp.Data;
using LoopBreakers.WebApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoopBreakers.WebApp.Repositories
{
    public class TransfersRepository : ITransfersRepository
    {
        private readonly ApplicationDbContext _db;
        public TransfersRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Transfer entity)
        {
            await _db.Transfers.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Transfer entity)
        {
             _db.Transfers.Remove(entity);
            return await Save();
        }

        public async Task<IList<Transfer>> FindAll()
        {
            var transfers = await _db.Transfers.ToListAsync();
            return transfers;
        }

        public async Task<Transfer> FindById(int id)
        {
            var transfer = await _db.Transfers
                .FirstOrDefaultAsync(q => q.Id == id);
            return transfer;
        }

        public async Task<IList<Transfer>> FindByDates(DateTime dateFrom, DateTime dateTo)
        {
            var transfers = await _db.Transfers
                .Where(q => q.Created >= dateFrom && q.Created <= dateTo).ToListAsync();
            return transfers;
        }

        public async Task<IList<Transfer>> FindByName(string searchName)
        {
            var transfers = await _db.Transfers
                .Where(n => n.LastName.ToLower() == searchName.ToLower()).ToListAsync();
            return transfers;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Transfers.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Transfer entity)
        {
            _db.Transfers.Update(entity);
            return await Save();
        }
    }
}
