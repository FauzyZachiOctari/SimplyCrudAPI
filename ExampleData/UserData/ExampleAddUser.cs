using SimplyCrudAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.UserData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfile))]
    public class ExampleAddUser : IExamplesProvider<UserProfileRegister>
    {
        public UserProfileRegister GetExamples() { 
            return new UserProfileRegister
            {
                LoginID = "admin",
                Password = "admin123",
                Email = "admin@gmail.com",
                Address = "Pekanbaru",
                Gender = "Pria",
                Age = 19
            }; 
        }
    }
}
