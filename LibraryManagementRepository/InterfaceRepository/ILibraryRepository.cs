using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface ILibraryRepository /*: IRepository<Library>*/
    {
        // Add any additional Library-specific methods here if needed
        Task<IEnumerable<Library>> GetAllAsync();
        Task<Library> GetByIdAsync(int id);
        Task AddAsync(Library entity);
        Task UpdateAsync(Library entity);
        Task DeleteAsync(int id);
    }

}
