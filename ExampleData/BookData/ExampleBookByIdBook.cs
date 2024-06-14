using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDataList))]
    public class ExampleBookByIdBook : IExamplesProvider<BookDataList>
    {
        public BookDataList GetExamples()
        {
            return new BookDataList
            {
                IdBook = new Guid("607459D3-4D45-4371-F042-08DC80850D69"),
                BookTittle = "Perjalanan Menuju Amsterdam",
                Writer = "Sulistiya Rahma",
                Publisher = "Cipta Karya Indonesia",
                PublicationYear = "2019",
                ISBN = "421-098-62-45-2",
                Stock = "5",
                RackNumber = "Rak 3D Lantai 2 Gedung II"
            };
        }
    }
}
