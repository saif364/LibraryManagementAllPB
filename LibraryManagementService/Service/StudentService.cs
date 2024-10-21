using AutoMapper;
using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;
using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementService.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Service
{
    public class StudentService : BaseService<Student>, IStudentService
    {
        private readonly LibraryDbContext _context;
        private readonly IStudentRepository _studentRepository;
        private readonly IAuditTrialBaseRepository<StudentAuditTrial> _auditTrialBaseRepository;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository, LibraryDbContext context, IAuditTrialBaseRepository<StudentAuditTrial> auditTrialBaseRepository, IMapper mapper)
            : base(studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _auditTrialBaseRepository = auditTrialBaseRepository;
            _mapper = mapper;
        }

        public async Task AddAsyncWithAT(Student student)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                await _studentRepository.AddAsync(student);

                var auditStudent = _mapper.Map<StudentAuditTrial>(student);
                auditStudent.CreatedDate = DateTime.Now;
                auditStudent.Action = EnumAction.Create.ToString();
                auditStudent.ActionBy = "Saif";
                await _auditTrialBaseRepository.AddAsync(auditStudent);

                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }


        }

        public async Task UpdateAsyncWithAT(Student student)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                await _studentRepository.UpdateAsync(student);

                var auditStudent = _mapper.Map<StudentAuditTrial>(student);
                auditStudent.UpdatedDate = DateTime.Now;
                auditStudent.Action = EnumAction.Update.ToString();
                auditStudent.UpdatedBy = "Saif";
                await _auditTrialBaseRepository.AddAsync(auditStudent);

                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }


        }

        public async Task DeleteAsyncWithAT(int id)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                await _studentRepository.DeleteAsync(id);

                var obj = _studentRepository.GetByIdAsync(id);
                var auditStudent = _mapper.Map<StudentAuditTrial>(obj);
                auditStudent.UpdatedDate = DateTime.Now;
                auditStudent.Action = EnumAction.Delete.ToString();
                auditStudent.DeletedBy = "Saif";
                await _auditTrialBaseRepository.AddAsync(auditStudent);

                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }


        }



        public async Task<TAudit> CreateAuditTrailAsync<TAudit, TSource>(TSource obj, EnumAction action, string deletedBy)
    where TAudit : class, new()
        {
            // Map the object to its audit trail counterpart
            var auditObject = _mapper.Map<TAudit>(obj);

            // Set common audit properties (assuming they are present in the audit object)
            var auditProperties = auditObject.GetType().GetProperties();
             
            if (action == EnumAction.Create)
            {
                var ActionBy = auditProperties.FirstOrDefault(p => p.Name == "ActionBy");
                if (ActionBy != null)
                {
                    ActionBy.SetValue(auditObject, "Saif");
                }
            }

            var updatedDateProp = auditProperties.FirstOrDefault(p => p.Name == "UpdatedDate");
            if (updatedDateProp != null)
            {
                updatedDateProp.SetValue(auditObject, DateTime.Now);
            }

            var actionProp = auditProperties.FirstOrDefault(p => p.Name == "Action");
            if (actionProp != null)
            {
                actionProp.SetValue(auditObject, action);
            }

            var deletedByProp = auditProperties.FirstOrDefault(p => p.Name == "DeletedBy");
            if (deletedByProp != null)
            {
                deletedByProp.SetValue(auditObject, deletedBy);
            }

            // Save the audit object using the repository
            await _auditTrialBaseRepository.AddAsync(auditObject);

            // Return the audit object
            return auditObject;
        }


        // Additional methods specific to StudentService can go here, if needed
    }

}
