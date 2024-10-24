using AutoMapper;
using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;
using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LibraryManagementService.Service
{
    public class StudentService : BaseService<Student>, IStudentService
    {
        private string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

        private readonly IStudentRepository _studentRepository;

        //no logic so direct repository call
        private readonly IRepository<StudentAuditTrial> _StudentAuditTrial;
        private readonly IRepository<StudentSubCourse> _StudentSubCourse;
        private readonly IRepository<StudentSubAttachment> _SubAttachmentRepo;
        private readonly IRepository<StudentSubCourseAuditTrial> _StudentSubCourseAuditTrial;
        private readonly IRepository<StudentSubAttachmentAuditTrial> _SubAttachmentATRepo;
        //
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository, IRepository<StudentAuditTrial> studentAuditTrial, IMapper mapper, IRepository<StudentSubCourse> studentSubCourse, IRepository<StudentSubCourseAuditTrial> studentSubCourseAuditTrial, IRepository<StudentSubAttachment> subAttachmentRepo, IRepository<StudentSubAttachmentAuditTrial> subAttachmentATRepo)
            : base(studentRepository)
        {

            _studentRepository = studentRepository;
            _StudentAuditTrial = studentAuditTrial;
            _mapper = mapper;
            _StudentSubCourse = studentSubCourse;
            _StudentSubCourseAuditTrial = studentSubCourseAuditTrial;
            _SubAttachmentRepo = subAttachmentRepo;
            _SubAttachmentATRepo = subAttachmentATRepo;
        }



        public async Task AuditTrial<T, TAudit>(T obj, EnumStatus status, string by, IRepository<TAudit> auditRepository)
      where T : class
      where TAudit : class, new()
        {
            var audit = _mapper.Map<TAudit>(obj);

            var auditEntity = audit as dynamic;
            auditEntity.CreatedDate = DateTime.Now;
            auditEntity.Action = status.ToString();
            auditEntity.ActionBy = by;

            await auditRepository.AddAsyncWithTransaction(auditEntity);
        }
        public async Task AddAsyncWithAT(StudentVM studentVM)
        {
            await BeginTransactionAsync();
            try
            {
                var student = _mapper.Map<Student>(studentVM);
                student.Status = EnumAction.Create.ToString();
                await _studentRepository.AddAsyncWithTransaction(student);
                //audit
                await AuditTrial(student, EnumStatus.Created, "Saif", _StudentAuditTrial);

                await SaveChangesAsyncWithTransaction();
                await CommitTransactionAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
        }
        public async Task UpdateAsyncWithAT(StudentVM studentVM, EnumStatus status = EnumStatus.Updated)
        {

            var student = _mapper.Map<Student>(studentVM);
            if (status == default)
            {
                student.Status = EnumStatus.Updated.ToString();
            }
            else
            {
                student.Status = status.ToString();
            }

            try
            {
                await BeginTransactionAsync();

                //parent table and audit
                await _studentRepository.UpdateAsyncWithTransaction(student);//1
                await AuditTrial(student, status, "Saif", _StudentAuditTrial);//2

                //child table and audit
                if (studentVM.StudentSubCourses != null)
                {
                    //delete by momid
                    await _StudentSubCourse.DeleteByMomIdAsyncWithTransaction(studentVM.Id);//3
                    foreach (var item in studentVM.StudentSubCourses)
                    {
                        var course = _mapper.Map<StudentSubCourse>(item);
                        course.MomId = studentVM.Id;
                        await _StudentSubCourse.AddAsyncWithTransaction(course);//4
                        //Audit
                        await AuditTrial(course, EnumStatus.Created, "Khalid", _StudentSubCourseAuditTrial);//6
                    }
                }

                //child file save and audit
                if (studentVM.StudentSubAttachmentsFiles != null)
                {
                    foreach (IFormFile file in studentVM.StudentSubAttachmentsFiles)
                    {
                        var obj = new StudentSubAttachment();
                        obj.OriginalFileName = file.FileName;
                        obj.UploadBy = "Khalid";
                        obj.UploadDate = DateTime.Now;
                        obj.MomID = studentVM.Id.ToString();

                        var fileName = FileHelper.SaveFile(file, uploadPath);
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            obj.FileNameInServer = fileName;
                            await _SubAttachmentRepo.AddAsyncWithTransaction(obj);//7
                            await AuditTrial(obj, EnumStatus.Created, "Khalid", _SubAttachmentATRepo);//8
                        }
                    }
                }

                await SaveChangesAsyncWithTransaction();
                await CommitTransactionAsync();

            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
        }
        public async Task DeleteAsyncWithAT(int id)
        {
            await BeginTransactionAsync();
            try
            {
                await _studentRepository.DeleteAsync(id);
                var obj = await _studentRepository.GetByIdAsync(id);
                await AuditTrial<Student, StudentAuditTrial>(obj, EnumStatus.Deleted, "Saif", _StudentAuditTrial);

                await SaveChangesAsyncWithTransaction();
                await CommitTransactionAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<StudentVM> GetByIdAsyncAT(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            var vmStudent = _mapper.Map<StudentVM>(student);

            var auditList = _StudentAuditTrial.GetAllAsyncQuery().Where(x => x.Id == id).OrderBy(x => x.Id).Take(20).ToList();

            vmStudent.StudentAuditTrials = _mapper.Map<List<BaseAuditTrialVM>>(auditList);
            var courses = _StudentSubCourse.GetAllAsyncQuery().Where(x => x.MomId == id).ToList();
            vmStudent.StudentSubCourses = _mapper.Map<List<StudentSubCourseVM>>(courses);
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
