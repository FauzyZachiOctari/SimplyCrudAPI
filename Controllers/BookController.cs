using SimplyCrudAPI.Data;
using SimplyCrudAPI.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using SimplyCrudAPI.ExampleData.BookData;

namespace SimplyCrudAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BookAPIDbContext _dbContext;

        public BookController(IConfiguration configuration, BookAPIDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        //untuk register user controller. ubah disini untuk register user
        /// <summary>
        /// Create a new data book. Use generated token while login to create a new book.
        /// </summary>
        /// <response code="200">This API is used to create a new data book. The input data below is an example of the values ​​that will be stored.</response>
        [HttpPost("AddBook")]
        [Authorize]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookAddBad), 400)]
        [ProducesResponseType(typeof(BookAddMessage), 200)]
        public async Task<IActionResult> AddBookData(BookDataListCreated bookDataList)
        {
            try
            {
                if (bookDataList == null ||
                    string.IsNullOrEmpty(bookDataList.BookTittle) ||
                    string.IsNullOrEmpty(bookDataList.Writer) ||
                    string.IsNullOrEmpty(bookDataList.Publisher) ||
                    string.IsNullOrEmpty(bookDataList.PublicationYear) ||
                    string.IsNullOrEmpty(bookDataList.ISBN) ||
                    string.IsNullOrEmpty(bookDataList.Stock) ||
                    string.IsNullOrEmpty(bookDataList.RackNumber))
                {
                    return BadRequest(new
                    {
                        message = "All fields are required and cannot be null."
                    });
                }

                var bookDataListCreate = new BookDataList()
                {
                    BookTittle = bookDataList.BookTittle,
                    Writer = bookDataList.Writer,
                    Publisher = bookDataList.Publisher,
                    PublicationYear = bookDataList.PublicationYear,
                    ISBN = bookDataList.ISBN,
                    Stock = bookDataList.Stock,
                    RackNumber = bookDataList.RackNumber,

                };
                await _dbContext.BookDataListed.AddAsync(bookDataListCreate);
                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    message = "User registered successfully",
                    userProfile = bookDataListCreate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
