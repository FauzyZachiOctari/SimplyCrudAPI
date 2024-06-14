using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BookNotFound))]
    public class ExampleBookNotFound : IExamplesProvider<BookNotFound>
    {
        public BookNotFound GetExamples()
        {
            return new BookNotFound
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                title = "Not Found",
                status = 404,
                traceId = "00-73b781880b2bad7a9698ecadd580ff3a-77599657d403b0a4-00"
            };
        }
    }
}
