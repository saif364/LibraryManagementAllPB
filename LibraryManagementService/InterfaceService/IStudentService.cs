using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.InterfaceService
{
    public interface IStudentService:IBaseService<Student>
    {
        Task AddAsyncWithAT(StudentVM student);
        Task UpdateAsyncWithAT(StudentVM student, EnumStatus status = EnumStatus.Updated);

        Task<StudentVM> GetByIdAsyncAT(int id);
        Stream GetFileStream(int id, out string fileName, out string contentType);

        Task StatusChange(EnumStatus status, int id);
    }

 
}
