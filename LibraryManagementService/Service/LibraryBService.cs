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
    public class LibraryBService : BaseService<Library>, ILibraryBService
    {
        public LibraryBService(ILibraryBRepository LibraryRepository)
            : base(LibraryRepository)
        {
        }

        // Additional methods specific to LibraryService can go here, if needed
    }

}
