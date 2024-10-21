

using LibraryManagement.Controllers;
using LibraryManagementModels.Entities;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Controllers
{

    public class BookController : BaseController
    {

        private string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");


        private readonly IBookService _BookService;
        private readonly IStudentService _studentService;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger,IBookService BookService, IStudentService studentService)
        {
            _BookService = BookService;
            _studentService = studentService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("This is an informational log message");
            var list = await _BookService.GetAllAsync();
            return View(list.ToList());
        }

        public IActionResult Create()
        {
            ReturnViewBags();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book Book)
        {
            try
            {
                 
                await _BookService.AddAsync(Book); //db related
                return JsonSuccess("Data Saved successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }

        public async Task< IActionResult> Update(int id)
        {
            var Book =await _BookService.GetByIdAsync(id);
            ReturnViewBags();
            return View(Book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book Book)
        {
            try
            {
                await _BookService.UpdateAsync(Book);
                //log
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
               await _BookService.DeleteAsync(id);
                return JsonSuccess("Data Deleted successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }
        public async Task< IActionResult> BookDetails(int id)
        {
            var Book = await _BookService.GetByIdAsync(id);
            return View(Book);
        }

        public void ReturnViewBags()
        {
            
            ViewBag.ListOfStudents = _studentService.GetAllAsync().Result.Select(x=> x.Name).ToList();
            //ViewBag.ListOfCountries = new List<SelectListItem>
            //{
            //    new SelectListItem{Text="Bangladesh",Value="Bangladesh"},
            //    new SelectListItem{Text="USA",Value="USA"},
            //    new SelectListItem{Text="Japan",Value="Japan"}
            //};
            ViewBag.ListOfCountries = _studentService.GetAllAsync().Result.Select(x => new SelectListItem { Text = x.Name, Value = x.Name });
        }

    }
}
