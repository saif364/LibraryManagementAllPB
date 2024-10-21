using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.InterfaceService
{
    public interface ILibraryService   
    {
        Task<IEnumerable<Library>> GetAllAsync();
        Task<Library> GetByIdAsync(int id);
        Task AddAsync(Library entity);
        Task UpdateAsync(Library entity);
        Task DeleteAsync(int id);

        Task AddLibraryWithTransactionAsync(Library library);
    }
}
