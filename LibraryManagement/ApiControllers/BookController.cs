using LibraryManagementModels.Entities;
using LibraryManagementService.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        // GET: api/Book/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookService.AddAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { message="Successful Creation"});
        }

        // PUT: api/Book/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
                return NotFound();

            await _bookService.UpdateAsync(book);
            return NoContent();
        }

        // DELETE: api/Book/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
