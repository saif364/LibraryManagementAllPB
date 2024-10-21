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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(LibraryDbContext context) : base(context) { }
    }

}
