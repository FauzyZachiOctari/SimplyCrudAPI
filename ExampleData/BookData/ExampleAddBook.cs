using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Book;
using SimplyCrudAPI.Models.User;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDataList))]
    public class ExampleAddBook : IExamplesProvider<BookDataListCreated>
    {
        public BookDataListCreated GetExamples()
        {
            return new BookDataListCreated
            {
                BookTittle = "Belajar Machine Learning",
                Writer = "Rifkie Primartha",
                Publisher = "Informatika Bandung",
                PublicationYear = "2018",
                ISBN = "978-602-6232-67-0",
                Stock = "12",
                RackNumber = "3A Machine Learning dan AI"
            };
        }
    }
}