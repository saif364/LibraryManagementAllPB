using AutoMapper;
using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LibraryManagementModels.BusinessModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapping one class to another 
            //for audit trial
            CreateMap<Student, StudentAuditTrial>();
            CreateMap<StudentAuditTrial, Student>();
            //for VM
            CreateMap<Student, StudentVM>();
            CreateMap<StudentVM, Student>();
            //

            //Mapping one class to another 
            //For audit trial
            CreateMap<StudentSubCourse, StudentSubCourseAuditTrial>();
            CreateMap<StudentSubCourseAuditTrial, StudentSubCourse>();
            //For VM
            CreateMap<StudentSubCourse, StudentSubCourseVM>();
            CreateMap<StudentSubCourseVM, StudentSubCourse>();
            //
        }
    }
}
