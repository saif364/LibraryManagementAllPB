using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface IRepository<T> where T : class
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsyncWithTransaction();

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        IQueryable<T> GetAllAsyncQuery();
         
        Task AddAsyncWithTransaction(T entity);
        Task UpdateAsyncWithTransaction(T entity);
        Task DeleteAsyncWithTransaction(int id);
    }

}
