using SimplyCrudAPI.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplyCrudAPI.Models.Book
{
    public class BookDataList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdBook { get; set; }
        public string BookTittle { get; set; }
        public string Writer { get; set; }
        public string Publisher { get; set; }
        public string PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Stock { get; set; }
        public string RackNumber { get; set; }
    }

    public class BookDataListCreated
    {
        public string BookTittle { get; set; }
        public string Writer { get; set; }
        public string Publisher { get; set; }
        public string PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Stock { get; set; }
        public string RackNumber { get; set; }
    }

    public class UpdateBookRequest
    {
        public Guid IdBook { get; set; }
        public string? BookTittle { get; set; }
        public string? Writer { get; set; }
        public string? Publisher { get; set; }
        public string? PublicationYear { get; set; }
        public string? ISBN { get; set; }
        public string? Stock { get; set; }
        public string? RackNumber { get; set; }
    }

    public class BookAddBad
    {
        public string Message { get; set; }
    }

    public class BookDataListCreatedSucces
    {
        public Guid IdBook { get; set; }
        public string BookTittle { get; set; }
        public string Writer { get; set; }
        public string Publisher { get; set; }
        public string PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Stock { get; set; }
        public string RackNumber { get; set; }
    }

    public class BookAddMessage
    {
        public string Message { get; set; }
        public BookDataListCreatedSucces BookDataList { get; set; }
    }

    public class BookNotFound
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }

    public class BookDeleted
    {
        public string Message { get; set; }
    }
}
