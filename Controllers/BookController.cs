﻿using SimplyCrudAPI.Data;
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
using System.Net;
using SimplyCrudAPI.Models.Global;
using SimplyCrudAPI.ExampleData.GlobalData;

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

        //untuk Added Book controller. ubah disini untuk Added Book
        /// <summary>
        /// Create a new data book. Use generated token while login to create a new book.
        /// </summary>
        /// <response code="200">This API is used to create a new data book. The input data below is an example of the values ​​that will be stored.</response>
        [HttpPost("AddBook")]
        //[Authorize]
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

        /// <summary>
        /// Gets all Book Information
        /// </summary>
        /// <returns>The API output will contain an arrays of carrier info.</returns>
        /// <response code="200">The API output will contain an arrays of Book info.</response>
        /// 
        ///// <response code="404">Invalid data Payload.</response>
        /// <response code="401">Bad Request to Server</response>
        [HttpGet("getbook")]
        [Authorize]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ExampleBook>), 200, Type = typeof(BookDataList[]))]
        [ProducesResponseType(typeof(IEnumerable<ExampleInvalidToken>), 401, Type = typeof(CheckTokenInvalid))]
        public async Task<IActionResult> GetAddress()
        {
            return Ok(await _dbContext.BookDataListed.ToListAsync());
        }

        /// <summary>
        /// Gets Book Information By Id Book
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The API output will contain an Book info which search By Id Book.</response>
        /// <response code="404">Data Not Found.</response>
        [HttpGet]
        [Route("GetBookByIdBook/{idBook:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ExampleBookNotFound>), 404, Type = typeof(BookNotFound))]
        [ProducesResponseType(typeof(IEnumerable<ExampleBookByIdBook>), 200, Type = typeof(BookDataList))]
        public async Task<IActionResult> GetAddressed([FromRoute] Guid idBook)
        {
            var book = await _dbContext.BookDataListed.FindAsync(idBook);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Update by Id Book.
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns>True if the Book is successfully updated, otherwise false.</returns>
        /// <response code="200">The result will display the data with changes. If any fields, isn't change. the field of old data will display.</response>
        /// <response code="400">The updateBookRequest field is required. Use ("Fill Your Data")</response>
        /// <response code="404">Data Not Found.</response>
        /// <remarks>
        /// Id Book must be filled in to update Book. Leave it null if you don't want to change the data. And if you want to change the data, replace null with "Your data". Use ("") if you update on API.
        /// </remarks>
        [HttpPut]
        [Route("UpdateBook")]
        [ProducesResponseType(typeof(IEnumerable<ExampleBookNotFound>), 404, Type = typeof(BookNotFound))]
        [ProducesResponseType(typeof(BookUpdatedUndocumented), 400)]
        [ProducesResponseType(typeof(BookUpdatedMessage), 200)]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest updateBookRequest)
        {
            try
            {
                var book = await _dbContext.BookDataListed.FindAsync(updateBookRequest.IdBook);

                if (book != null)
                {
                    if (updateBookRequest.BookTittle != null)
                        book.BookTittle = updateBookRequest.BookTittle;

                    if (updateBookRequest.Writer != null)
                        book.Writer = updateBookRequest.Writer;

                    if (updateBookRequest.Publisher != null)
                        book.Publisher = updateBookRequest.Publisher;

                    if (updateBookRequest.PublicationYear != null)
                        book.PublicationYear = updateBookRequest.PublicationYear;

                    if (updateBookRequest.ISBN != null)
                        book.ISBN = updateBookRequest.ISBN;

                    if (updateBookRequest.Stock != null)
                        book.Stock = updateBookRequest.Stock;

                    if (updateBookRequest.RackNumber != null)
                        book.RackNumber = updateBookRequest.RackNumber;

                    await _dbContext.SaveChangesAsync();

                    return Ok(new
                    {
                        message = "Update Book successfully",
                        Address = book
                    });
                }

                return NotFound();
            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an Book By Id Book.
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns>True if the Book is successfully deleted, otherwise false.</returns>
        /// <remarks>
        /// Id Book must be filled in. If the ID Book is found, the Book data will be deleted
        /// </remarks>
        [HttpDelete]
        [Route("DeleteBook/{idBook:guid}")]
        //[Route("DeleteBook")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(typeof(IEnumerable<ExampleBookDeleted>), 200, Type = typeof(BookDeleted))]
        [ProducesResponseType(typeof(IEnumerable<ExampleBookNotFound>), 404, Type = typeof(BookNotFound))]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid idBook)
        {
            try
            {
                var book = await _dbContext.BookDataListed.FindAsync(idBook);

                if (book != null)
                {
                    _dbContext.Remove(book);
                    await _dbContext.SaveChangesAsync();
                    return Ok(new
                    {
                        message = "Book Deleted successfully"
                    });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }
    }
}
