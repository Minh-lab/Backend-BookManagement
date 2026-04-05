using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;
using Microsoft.AspNetCore.Mvc;



namespace BackendAPIASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBookAsync();
            if (books == null) return NotFound("Không có sách nào trong hệ thống.");

            return Ok(books);
        }


    }

}
