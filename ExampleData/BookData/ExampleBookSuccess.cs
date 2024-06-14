using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDataList))]
    public class ExampleBookSuccess : IExamplesProvider<BookAddMessage>
    {
        public BookAddMessage GetExamples()
        {
            return new BookAddMessage
            {
                BookDataList = new BookDataListCreatedSucces
                {
                    IdBook = new Guid("EF25D631-9A9C-40B0-B32D-0871D0110DA0"),
                    BookTittle = "Book Tittle",
                    Writer = "Book Writer",
                    Publisher = "Publisher",
                    PublicationYear = "Only Year",
                    ISBN = "918-604-6232-67-0",
                    Stock = "12",
                    RackNumber = "4A Lantai 1 Gedung III"
                },
                Message = "Added Book successfully"
            };
        }
    }
}
