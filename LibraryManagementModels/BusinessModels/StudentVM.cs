using LibraryManagementModels.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementModels.BusinessModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }


        public List<StudentAuditTrial> StudentAuditTrials { get; set; }

    }
}
