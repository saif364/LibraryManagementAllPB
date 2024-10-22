using LibraryManagementModels.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementModels.BusinessModels
{
    public class StudentVM :Student
    {
        public List<StudentAuditTrial>? StudentAuditTrials { get; set; } = new List<StudentAuditTrial>();
        public List<StudentSubCourseVM>? StudentSubCourses { get; set; } = new List<StudentSubCourseVM> { };

    }
}
