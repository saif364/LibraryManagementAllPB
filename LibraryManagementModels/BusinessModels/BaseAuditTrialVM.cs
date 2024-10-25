using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementModels.BusinessModels
{
    public class BaseAuditTrialVM
    {

        [DisplayName("Action By")]
        public string? ActionBy { get; set; }
        [DisplayName("Date")]
        public DateTime CreatedDate { get; set; }
        public string? Action { get; set; }

        public int AuditTrialId { get; set; }


    }
}
