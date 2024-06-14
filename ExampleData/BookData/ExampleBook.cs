using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDataList[]))]
    public class ExampleBook : IExamplesProvider<BookDataList[]>
    {
        public BookDataList[] GetExamples()
        {
            return new BookDataList[]
            {
                new BookDataList
                {
                    IdBook = new Guid("607459D3-4D45-4371-F042-08DC80850D69"),
                    BookTittle = "Perjalanan Menuju Amsterdam",
                    Writer = "Sulistiya Rahma",
                    Publisher = "Cipta Karya Indonesia",
                    PublicationYear = "2019",
                    ISBN = "421-098-62-45-2",
                    Stock = "5",
                    RackNumber = "Rak 3D Lantai 2 Gedung II"
                },
                new BookDataList
                {
                    IdBook = new Guid("82831FCD-02AB-4658-5D1C-08DC8085C5AB"),
                    BookTittle = "Learning English for Adult",
                    Writer = "Andi Setiawan",
                    Publisher = "Media Belajar Jogjakarta",
                    PublicationYear = "2014",
                    ISBN = "142-850-12-33-2",
                    Stock = "12",
                    RackNumber = "Rak 3B Lantai 2 Gedung II"
                },
            };
        }
    }
}
