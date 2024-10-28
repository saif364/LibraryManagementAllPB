using AutoMapper;
using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;



namespace LibraryManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            //for VM
            CreateMap<Student, StudentVM>();
            CreateMap<StudentVM, Student>();

            CreateMap<StudentSubCourse, StudentSubCourseVM>();
            CreateMap<StudentSubCourseVM, StudentSubCourse>();

            //For audit trial
            CreateMap<Student, StudentAuditTrial>();
            CreateMap<StudentAuditTrial, BaseAuditTrialVM>();

            CreateMap<StudentSubCourse, StudentSubCourseAuditTrial>();
            CreateMap<StudentSubCourseAuditTrial, StudentSubCourse>();

            CreateMap<StudentSubAttachment, StudentSubAttachmentAuditTrial>();
            CreateMap<StudentSubAttachment, StudentSubAttachmentVM>();
            //
        }
    }
}
