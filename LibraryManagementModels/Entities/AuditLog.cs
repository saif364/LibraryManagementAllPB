
namespace LibraryManagementModels.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; } // "Create", "Update", "Delete"
        public string User { get; set; } // The user who made the change
        public DateTime Timestamp { get; set; }
        public string OldValues { get; set; } = "";
        public string NewValues { get; set; } = "";
        public string PrimaryKey { get; set; } // ID of the changed entity
    }
}
