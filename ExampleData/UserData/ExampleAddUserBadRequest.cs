using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.User;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.UserData
{
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UserProfile))]
    public class ExampleAddUserBadRequest : IExamplesProvider<UserProfileRegisterBad>
    {
        public UserProfileRegisterBad GetExamples()
        {
            return new UserProfileRegisterBad()
            {
                Message = "All fields are required and cannot be null."
            };
        }
    }
}