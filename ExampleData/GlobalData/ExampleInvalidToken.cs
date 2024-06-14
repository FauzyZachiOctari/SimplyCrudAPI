using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Global;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.GlobalData
{
    public class ExampleInvalidToken : IExamplesProvider<CheckTokenInvalid>
    {
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CheckTokenInvalid))]
        public CheckTokenInvalid GetExamples()
        {
            return new CheckTokenInvalid
            {
                userMessage = "Invalid Token",
            };
        }
    }
}
