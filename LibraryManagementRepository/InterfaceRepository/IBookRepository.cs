using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        // Add any additional Book-specific methods here if needed
    }

}
