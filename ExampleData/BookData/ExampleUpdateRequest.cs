using SimplyCrudAPI.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.BookData
{
    public class ExampleUpdateRequest : IExamplesProvider<UpdateBookRequest>
    {
        public UpdateBookRequest GetExamples()
        {
            return new UpdateBookRequest
            {
                IdBook = new Guid("EF25D631-9A9C-40B0-B32D-0871D0110DA0"),
                BookTittle = null,
                Writer = null,
                Publisher = null,
                PublicationYear = null,
                ISBN = null,
                Stock = null,
                RackNumber = null
            };
        }
    }
}
