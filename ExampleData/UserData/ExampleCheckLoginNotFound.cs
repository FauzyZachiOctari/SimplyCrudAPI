using SimplyCrudAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.UserData
{
    public class ExampleCheckLoginNotFound : IExamplesProvider<CheckLoginExampleNotFound>
    {
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CheckLoginExampleNotFound))]
        public CheckLoginExampleNotFound GetExamples()
        {
            return new CheckLoginExampleNotFound
            {
                userMessage = "Login Failed",
            };
        }
    }
}
