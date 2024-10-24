using AutoMapper;
using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;



namespace LibraryManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           

            //Mapping one class to another 
            //for audit trial
            CreateMap<Student, StudentAuditTrial>();
           
            //for VM
            CreateMap<Student, StudentVM>();
            CreateMap<StudentVM, Student>();
            CreateMap<StudentAuditTrial , BaseAuditTrialVM>();
            //

            //Mapping one class to another 
            //For audit trial
            CreateMap<StudentSubCourse, StudentSubCourseAuditTrial>();
            CreateMap<StudentSubCourseAuditTrial, StudentSubCourse>();
            //CreateMap<List<StudentSubCourse>, List<StudentSubCourseAuditTrial>>();
            //CreateMap<List<StudentSubCourseAuditTrial>, List<StudentSubCourse>>();
            //For VM
            CreateMap<StudentSubCourse, StudentSubCourseVM>();
            CreateMap<StudentSubCourseVM, StudentSubCourse>();
            //CreateMap<List<StudentSubCourse>, List<StudentSubCourseVM>>();
            //CreateMap<List<StudentSubCourseVM>, List<StudentSubCourse>>();
            
            //
        }
    }
}
