using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SimplyCrudAPI.Models.User
{
    public class UserLogin
    {
        [Key]
        public string LoginID { get; set; }
        public string Password { get; set; }
    }
}
