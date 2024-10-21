using LibraryManagementModels.Entities;
using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    { 
        public BookRepository(LibraryDbContext context) : base(context) { }
    }
            
}  
