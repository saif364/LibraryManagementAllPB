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
    public class LibraryBRepository : Repository<Library>, ILibraryBRepository
    {
        public LibraryBRepository(LibraryDbContext context) : base(context) { }

    }

}
