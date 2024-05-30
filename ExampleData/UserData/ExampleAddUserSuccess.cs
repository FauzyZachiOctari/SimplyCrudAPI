//using SimplyCrudAPI.Models.User;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.Filters;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Net;
//using System.Reflection;

//namespace SimplyCrudAPI.ExampleData.UserData
//{
//    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfile))]
//    public class ExampleAddUser : IExamplesProvider<UserProfileRegister>
//    {
//        public UserProfileRegister GetExamples() {
//            return new UserProfileRegister
//            {
//                UserProfile = new UserProfileRegisterBad
//                {
//                    Message = "User registered successfully"
//                },
//                IdUser = 3,
//                LoginID = "admin",
//                Password = "$2a$11$5IqEzHK0d1F16UK2Sk2H6ORtB1LDcpycyJVAQC4FCg7wXUxzQlkxu", // This should be a hashed password
//                Email = "admin@gmail.com",
//                Address = "Pekanbaru",
//                Gender = "Pria",
//                Age = 19
//            };
//        }
//    }
//}
