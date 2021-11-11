using LoopBreakers.WebApp.Data;
using LoopBreakers.WebApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoopBreakers.WebApp.Services
{
    public class TransfersRepository : ITransfersRepository
    {
        private readonly ApplicationDbContext _db;
        public TransfersRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<bool> Create(Transfer entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Transfer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Transfer>> FindAll()
        {
            var transfers = await _db.Transfers.ToListAsync();
            return transfers;
        }

        public Task<Transfer> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Transfer entity)
        {
            throw new NotImplementedException();
        }
    }
}
