using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GiamminLib.DomainModels
{
    public interface IRepository<T> where T : class, IDbEntity<int>
    {
        Task<int> InsertEntityAsync(T entity, int? editorId=null, CancellationToken cancellationToken=default);
        Task<int> InsertEntityAsync(IEnumerable<T> entities, int? editorId=null, CancellationToken cancellationToken=default);
        Task<bool> DeleteEntityAsync(T entity, int? editorId = null, CancellationToken cancellationToken = default);
        Task<bool> DeleteEntityAsync(IEnumerable<T> entities, int? editorId = null, CancellationToken cancellationToken = default);
        Task<bool> UpdateEntityAsync(T entity, int? editorId = null, CancellationToken cancellationToken = default);
        Task<bool> UpdateEntityAsync(IEnumerable<T> entities, int? editorId = null, CancellationToken cancellationToken = default);
    }
}