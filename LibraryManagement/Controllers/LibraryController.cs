
using LibraryManagementModels.Entities;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{

    public class LibraryController : BaseController
    {

        private string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");


        private readonly ILibraryService _LibraryService;
        private readonly IStudentService _studentService;
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILogger<LibraryController> logger,ILibraryService LibraryService, IStudentService studentService)
        {
            _LibraryService = LibraryService;
            _studentService = studentService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("This is an informational log message");
            _logger.LogError("This is an error log message");
            var list = await _LibraryService.GetAllAsync();
            return View(list.ToList());
        }

        public IActionResult Create()
        {
            ReturnViewBags();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Library library)
        {
            try
            {
                //multiple checkbox
                if (library.SelectedStudents.Any())
                    library.Students = string.Join("||", library.SelectedStudents);
                else
                    library.Students = string.Empty;
                //
                //file handle

                var result = FileHelper.SaveFile(library.UploadedFile, uploadPath);
                if (result == true)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("ddMMYYYYHHmmss") + "_" + library.UploadedFile.FileName;
                    library.FileName = library.UploadedFile.FileName;
                }
                else
                {
                    library.FileName = string.Empty;
                }
                //
                await _LibraryService.AddAsync(library); //db related
                return JsonSuccess("Data Saved successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }

        public async Task< IActionResult> Update(int id)
        {
            var library =await _LibraryService.GetByIdAsync(id);
            //library.SelectedStudents = library.Students.Split(new string[] { "||" }, StringSplitOptions.None).ToList();
            ReturnViewBags();
            return View(library);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Library library)
        {
            try
            {

                if (library.SelectedStudents.Any())
                    library.Students = string.Join("||", library.SelectedStudents);
                else
                    library.Students = string.Empty;
                //should delete and add always no update for file 
                if (library.UploadedFile != null)
                {
                    var result = FileHelper.SaveFile(library.UploadedFile, uploadPath);
                    if (result)
                    {
                        library.FileName = library.UploadedFile.FileName;
                    }
                }
                else
                {
                    var existingLibrary = await _LibraryService.GetByIdAsync(library.Id);
                    if (existingLibrary.FileName == null)
                    {
                        library.FileName = string.Empty;
                    }
                    else
                    {
                        library.FileName = existingLibrary.FileName;
                    }
                }
                await _LibraryService.UpdateAsync(library);
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
               await _LibraryService.DeleteAsync(id);
               return JsonSuccess("Data Deleted successfully", "Index");
            }
            catch (Exception ex)
            {
                return JsonInternalServerError(ex.InnerException.Message ?? ex.Message);
            }

        }
        public async Task< IActionResult> LibraryDetails(int id)
        {
            
            var library = await _LibraryService.GetByIdAsync(id);
            return View(library);
        }


        public async Task<IActionResult> DownloadFile(int id)
        {
            var library =await _LibraryService.GetByIdAsync(id);
            var fileName = library.FileName;

            if (library == null || string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            // Create the full path to the file
            var downloadPath = Path.Combine(uploadPath, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(downloadPath))
            {
                return NotFound();
            }
            var fileStream = FileHelper.FileStream(downloadPath);
            if (fileStream != null)
            {
                var contentType = FileHelper.GetContentType(fileName);
                return File(fileStream, contentType, library.FileName);
            }
            return NotFound();
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
