using AutoMapper;
using LibraryManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
 

namespace LibraryManagementModels.BusinessModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //for audit trial
            CreateMap<StudentVM, StudentAuditTrial>();
            CreateMap<StudentAuditTrial, StudentVM>();
        }
    }
}
