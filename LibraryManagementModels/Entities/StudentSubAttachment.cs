using System.ComponentModel.DataAnnotations;

namespace LibraryManagementModels.Entities
{
    public class StudentSubAttachment
    {
        [Key]
        public int Id { get; set; }
        public string MomID { get; set; }
        public string OriginalFileName { get; set; }
        public string? MIME { get; set; }
        public string FileNameInServer { get; set; }
        public string UploadBy { get; set; }
        public string? UploadByID { get; set; }
        public DateTime? UploadDate { get; set; }

    }

    public class StudentSubAttachmentAuditTrial : BaseAuditTrial
    {
        public string MomID { get; set; }
        public string OriginalFileName { get; set; }
        public string? MIME { get; set; }
        public string FileNameInServer { get; set; }
        public string UploadBy { get; set; }
        public string? UploadByID { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
