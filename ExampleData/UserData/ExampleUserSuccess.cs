﻿using SimplyCrudAPI.Models.Message;
using SimplyCrudAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.User;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.UserData
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileRegisterData))]
    public class ExampleUserSuccess : IExamplesProvider<UserProfileRegisterData>
    {
        public UserProfileRegisterData GetExamples()
        {
            return new UserProfileRegisterData
            {
                Message = "User registered successfully",
                IdUser = 1,
                LoginID = "admin",
                Password = "$2a$11$5IqEzHK0d1F16UK2Sk2H6OxfjsPel6vElJl2s7OC0TcnB5aOptEVy",
                Email = "admin@gmail.com",
                Address = "Pekanbaru",
                Gender = "Pria",
                Age = 19
            };
        }
    }
}