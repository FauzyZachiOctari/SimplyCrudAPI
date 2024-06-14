using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDeleted))]
    public class ExampleBookDeleted : IExamplesProvider<BookDeleted>
    {
        public BookDeleted GetExamples()
        {
            return new BookDeleted
            {
                Message = "Book Delete Successfully"
            };
        }
    }
}
