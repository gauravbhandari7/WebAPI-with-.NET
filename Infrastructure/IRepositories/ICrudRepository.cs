using Infrastructure.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface ICrudRepository<T>
    {
        Task<int> InsertEntity(T entity, CancellationToken cancellationToken);
        Task<int> UpdateEntity(T entity, CancellationToken cancellationToken);
        Task<int> Delete(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAll();
    }
}
