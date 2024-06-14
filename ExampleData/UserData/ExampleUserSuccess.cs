using SimplyCrudAPI.Models.Message;
using SimplyCrudAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.UserData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfile))]
    public class ExampleUserSuccess : IExamplesProvider<UserProfileRegisterMessage>
    {
        public UserProfileRegisterMessage GetExamples()
        {
            return new UserProfileRegisterMessage
            {
                UserProfile = new UserProfileRegisterData
                {
                    IdUser = 3,
                    LoginID = "admin",
                    Password = "$2a$11$5IqEzHK0d1F16UK2Sk2H6ORtB1LDcpycyJVAQC4FCg7wXUxzQlkxu", // This should be a hashed password
                    Email = "admin@gmail.com",
                    Address = "Pekanbaru",
                    Gender = "Pria",
                    Age = 19
                },
                Message = "User registered successfully"
            };
        }
    }
}
