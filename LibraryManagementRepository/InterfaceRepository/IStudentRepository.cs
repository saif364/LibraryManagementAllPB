using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementRepository.InterfaceRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        // Add any additional student-specific methods here if needed
    }

}
