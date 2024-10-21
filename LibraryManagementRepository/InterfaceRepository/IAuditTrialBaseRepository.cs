using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface IAuditTrialBaseRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(int id);
    }

}
