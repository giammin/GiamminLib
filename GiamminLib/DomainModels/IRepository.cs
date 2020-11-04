using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiamminLib.DomainModels
{
    public interface IRepository<T> where T : class, IDbEntity<int>
    {
        Task<int> Insert(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<int> InsertOrUpdate(T entity);
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
    }
}