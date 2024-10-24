using LibraryManagementModels.Entities;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementModels.BusinessModels
{
    public class StudentVM :Student
    {
        public List<BaseAuditTrialVM>? StudentAuditTrials { get; set; } = new List<BaseAuditTrialVM>();
        public List<StudentSubCourseVM>? StudentSubCourses { get; set; } = new List<StudentSubCourseVM> ();
        //public List<StudentSubAttachment>? StudentSubAttachments { get; set; } = new List<StudentSubAttachment> { };
        public List<IFormFile> StudentSubAttachmentsFiles { get; set; } = new List<IFormFile>();

    }
     
}
