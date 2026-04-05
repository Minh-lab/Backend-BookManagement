using BackendAPIASP.DTOs.Book;
using BackendAPIASP.Interfaces.Services;
using BackendAPIASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPIASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookController : ControllerBase
    {
        private readonly IUserBookService _userBookService;
        public UserBookController(IUserBookService userBookService)
        {
            this._userBookService = userBookService;
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> getUserLibrary(int userId)
        {
            var library = await _userBookService.GetUserLibrary(userId);
            return Ok(library);
        }
    }
}
