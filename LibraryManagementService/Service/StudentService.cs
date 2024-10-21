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

        public async Task UpdateAsyncWithAT(StudentVM studentVM)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var student = _mapper.Map<Student>(studentVM);
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

         
        public async Task<StudentVM> GetByIdATAsync(int id)
        {
            var student=await _studentRepository.GetByIdAsync(id);
            var vmStudent = _mapper.Map<StudentVM>(student);
            vmStudent.StudentAuditTrials = _auditTrialBaseRepository.GetAllAsync().Where(x=> x.Id==id).ToList();
            return vmStudent;
        }


        // Additional methods specific to StudentService can go here, if needed
    }


    
}
