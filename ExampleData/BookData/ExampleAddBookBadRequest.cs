using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection.Metadata.Ecma335;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BookDataList))]
    public class ExampleAddBookBadRequest : IExamplesProvider<BookAddBad>
    {
        public BookAddBad GetExamples()
        {
            return new BookAddBad()
            {
                Message = "All fields are required and cannot be null."
            };
        }
    }
}
