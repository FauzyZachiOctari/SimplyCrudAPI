using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookUpdatedMessage))]
    public class ExampleUpdateSuccess : IExamplesProvider<BookUpdatedMessage>
    {
        public BookUpdatedMessage GetExamples()
        {
            return new BookUpdatedMessage
            {
                Message = "Update Book successfully",
                BookDataUpdate = new BookDataListUpdatedSucces
                {
                    IdBook = new Guid("EF25D631-9A9C-40B0-B32D-0871D0110DA0"),
                    BookTittle = "Book Ini testing update",
                    Writer = "Book Writer",
                    Publisher = "Publisher",
                    PublicationYear = "Only Year",
                    ISBN = "918-604-6232-67-0",
                    Stock = "12",
                    RackNumber = "4A Lantai 1 Gedung III"
                }
            };
        }
    }
}
