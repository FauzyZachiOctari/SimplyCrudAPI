using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BookUpdatedUndocumented))]
    public class ExampleUpdateUndocumented : IExamplesProvider<BookUpdatedUndocumented>
    {
        public BookUpdatedUndocumented GetExamples()
        {
            return new BookUpdatedUndocumented
            {
                Message = "The updateBookRequest field is required."
            };
        }
    }
}
