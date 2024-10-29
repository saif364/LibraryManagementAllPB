using AutoMapper;
using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementRepository.Repository;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LibraryManagement.Controllers
{
    [Authorize]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly IRepository<StudentAuditTrial> _auditTrialBaseRepository;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger , IStudentService studentService, IMapper mapper, IRepository<StudentAuditTrial> auditTrialBaseRepository)
        {
            _studentService = studentService;
            _logger = logger;
            _mapper = mapper;
            _auditTrialBaseRepository = auditTrialBaseRepository;
        }
        public async Task<IActionResult> Index()
        {
           // _logger.LogError("This is an error log message");
            var list = await _studentService.GetAllAsync();
            return View(list.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentVM student)
        {
            try
            {
                await _studentService.AddAsyncWithAT(student); 
                return JsonSuccess("Data Saved successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }
        }
        public async Task<IActionResult> StatusChange(EnumStatus status,int id)
        {
            try
            {
                await _studentService.StatusChange(status, id);
                return JsonSuccess("Status Changed successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var vmStudent = await _studentService.GetByIdAsyncAT(id);
            return View(vmStudent);
        }
        [HttpPost]
        public async Task<IActionResult> Update(StudentVM student)
        {
            try
            {
                await _studentService.UpdateAsyncWithAT(student);
                return JsonSuccess("Data Updated successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.Message?? ex.InnerException.Message);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);
                return JsonSuccess("Data Deleted successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetByIdAsyncAT(id);
            return View(student);
        }

        public async Task<IActionResult> DownloadFile(int id)
        {
            string fileName;
            string contentType;
            var fileStream = _studentService.GetFileStream(id, out fileName, out contentType);

            if (fileStream == null || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(contentType))
            {
                return NotFound();
            }

            return File(fileStream, contentType, fileName);
        }
         
    }
}
