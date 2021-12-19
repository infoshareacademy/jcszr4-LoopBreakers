using System.Collections.Generic;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;

namespace LoopBreakers.DAL.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> isExists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
