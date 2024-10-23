using LibraryManagementModels.Entities;

namespace LibraryManagementModels.BusinessModels
{
    public class StudentVM :Student
    {
        public List<BaseAuditTrialVM>? StudentAuditTrials { get; set; } = new List<BaseAuditTrialVM>();
        public List<StudentSubCourseVM>? StudentSubCourses { get; set; } = new List<StudentSubCourseVM> { };

    }
}
