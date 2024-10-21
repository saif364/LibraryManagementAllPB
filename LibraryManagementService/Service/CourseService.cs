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
    public class CourseService : BaseService<Course>, ICourseService
    {
        public CourseService(ICourseRepository CourseRepository)
            : base(CourseRepository)
        {
        }

        // Additional methods specific to CourseService can go here, if needed
    }

}
