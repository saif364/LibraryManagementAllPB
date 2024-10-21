using LibraryManagementModels.Entities;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementService.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Service
{
    public class BookService : BaseService<Book>, IBookService
    {
        public BookService(IBookRepository BookRepository)
            : base(BookRepository)
        {
        }

        // Additional methods specific to BookService can go here, if needed
    }

}
