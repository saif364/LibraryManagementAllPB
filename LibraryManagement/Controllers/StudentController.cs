using LibraryManagementModels.Entities;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class StudentController : BaseController
    {

        
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger , IStudentService studentService)
        {
             
            _studentService = studentService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("This is an informational log message");
            _logger.LogError("This is an error log message");
            var list = await _studentService.GetAllAsync();
            return View(list.ToList());
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                await _studentService.AddAsyncWithAT(student); //db related
                return JsonSuccess("Data Saved successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            try
            {
                await _studentService.UpdateAsyncWithAT(student);
                return JsonSuccess("Data Updated successfully", "Index");

            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
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
            var student = await _studentService.GetByIdAsync(id);
            return View(student);
        }


    }
}
