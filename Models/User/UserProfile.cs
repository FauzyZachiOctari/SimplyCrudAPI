using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplyCrudAPI.Models.User
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }

    public class UserProfileRegister
    {
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }

    public class UserProfileRegisterData
    {
        public int IdUser { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Message { get; set; }
    }

    public class UserProfileRegisterBad
    {
        public string Message { get; set; }
    }
}
