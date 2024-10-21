using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface IAuditTrialBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task AddAsync<TAudit>(TAudit auditObject) where TAudit : class, new();
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(int id);
    }

}
