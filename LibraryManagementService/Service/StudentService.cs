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

        public async Task LogAuditAsync<T, TAudit>(T obj, EnumStatus action, string by)
    where T : class
    where TAudit : class, new()
        {
            var auditEntry = _mapper.Map<TAudit>(obj);
            var auditEntity = auditEntry as dynamic;
            auditEntity.UpdatedDate = DateTime.Now;
            auditEntity.Action = action.ToString();
            auditEntity.ActionBy = by;

            await _auditTrialBaseRepository.AddAsync(auditEntity);
        }

        public async Task AddAsyncWithAT(StudentVM studentVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var student = _mapper.Map<Student>(studentVM);
                student.Status = EnumAction.Create.ToString();
                await _studentRepository.AddAsync(student);
                //audit
                await LogAuditAsync<Student, StudentAuditTrial>(student, EnumStatus.Created, "Saif");
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsyncWithAT(StudentVM studentVM, EnumStatus status = EnumStatus.Updated)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var student = _mapper.Map<Student>(studentVM);
                if (status == default(EnumStatus))
                {
                    student.Status = EnumStatus.Updated.ToString();
                }
                else
                {
                    student.Status = status.ToString();
                }
                await _studentRepository.UpdateAsync(student);
                await LogAuditAsync<Student, StudentAuditTrial>(student, status, "Saif");
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
                var obj = await _studentRepository.GetByIdAsync(id);
                await LogAuditAsync<Student, StudentAuditTrial>(obj, EnumStatus.Deleted, "Saif");
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<StudentVM> GetByIdATAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            var vmStudent = _mapper.Map<StudentVM>(student);
            vmStudent.StudentAuditTrials = _auditTrialBaseRepository.GetAllAsync().Where(x => x.Id == id).ToList();
            return vmStudent;
        }
        public async Task StatusChange(EnumStatus status, int id)
        {
            var studentVm = await _studentRepository.GetByIdAsync(id);
            var studnet = _mapper.Map<StudentVM>(studentVm);
            await UpdateAsyncWithAT(studnet, status);
        }

    }
}
