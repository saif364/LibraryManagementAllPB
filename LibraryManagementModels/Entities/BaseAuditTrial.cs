using System.ComponentModel.DataAnnotations;

namespace LibraryManagementModels.Entities
{
    public  class BaseAuditTrial
    {
        [Key]
        public int AuditTrialId { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ActionBy { get; set; }
        public string? Action { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }
}
