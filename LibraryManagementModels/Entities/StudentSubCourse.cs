using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementModels.Entities
{
    public class StudentSubCourse
    {
        [Key]
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string TeacherName { get; set; }
        public int MaximumStudent { get; set; }


        public int StudentId {  get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

    }

    public class StudentSubCourseAuditTrial : BaseAuditTrial
    {
        public string? CourseName { get; set; }
        public string TeacherName { get; set; }
        public int MaximumStudent { get; set; }
        public int StudentId { get; set; }

    }
}
