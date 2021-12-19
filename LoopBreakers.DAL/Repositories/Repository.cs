using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;

namespace LoopBreakers.DAL.Repositories
{
    public class Repository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        private readonly Repository<Entity> _rep;

        public Repository(ApplicationDbContext context)
        {
            // Przyklad uzycia w serwisie
            //_rep = rep;
            //_rep.GetAllQueryable().Where(x => x.Created < DateTime.Now).ToListAsync();
            this.context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return entities.AsQueryable();
        }

        public IEnumerable<T> FindAll()
        {
            return entities.AsEnumerable();
        }
        public T FindById(int id)
        {
            return entities.SingleOrDefault(s => s.Id == id.ToString());
        }

        public Task<bool> isExists(int id)
        {
            return entities.AnyAsync(x => x.Id == id.ToString());
        }

        public Task<bool> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}
