using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace LoopBreakers.DAL.Repositories
{
    public class Repository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return entities.AsQueryable();
        }

        public async Task<IEnumerable<T>> FindAll()
        {
           return await entities.ToListAsync();
        }
        public async Task<T> FindById(int id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public Task<bool> isExists(int id)
        {
            return entities.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Create(T entity)
        {
            await entities.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(T entity)
        {
            entities.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(T entity)
        {
            var entityToDelete = entities.FirstOrDefault(x => x.Id == entity.Id);
            entities.Remove(entityToDelete);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
