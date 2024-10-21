using LibraryManagementModels.Entities;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class CourseController : BaseController
    {

        
        private readonly ICourseService _CourseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger , ICourseService CourseService)
        {
             
            _CourseService = CourseService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("This is an informational log message");
            _logger.LogError("This is an error log message");
            var list = await _CourseService.GetAllAsync();
            return View(list.ToList());
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course Course)
        {
            try
            {
                await _CourseService.AddAsync(Course); //db related
                return JsonSuccess("Data Saved successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var Course = await _CourseService.GetByIdAsync(id);
            return View(Course);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course Course)
        {
            try
            {
                await _CourseService.UpdateAsync(Course);
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
                await _CourseService.DeleteAsync(id);
                return JsonSuccess("Data Deleted successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }
        public async Task<IActionResult> Details(int id)
        {
            var Course = await _CourseService.GetByIdAsync(id);
            return View(Course);
        }


    }
}
