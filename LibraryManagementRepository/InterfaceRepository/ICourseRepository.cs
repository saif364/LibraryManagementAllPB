using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface ICourseRepository : IRepository<Course>
    {
        // Add any additional Course-specific methods here if needed
    }

}
