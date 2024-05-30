using System.ComponentModel.DataAnnotations;

namespace SimplyCrudAPI.Models.User
{
    public class LogCheckLogin
    {
        [Key]
        public Guid LogId { get; set; }
        public string userMessage { get; set; }
        public string userToken { get; set; }
        public string LoginID { get; set; }
        public string password { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    public class CheckLoginExample
    {
        public string userMessage { get; set; }
        public string userToken { get; set; }
        public CheckLoginExamplePassword userProfile { get; set; }
    }

    public class CheckLoginExamplePassword
    {
        public string password { get; set; }
    }

    public class CheckLoginExampleNotFound
    {
        public string userMessage { get; set; }
    }
}
