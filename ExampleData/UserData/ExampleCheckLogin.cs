using SimplyCrudAPI.Models.Message;
using SimplyCrudAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace SimplyCrudAPI.ExampleData.UserData
{
    public class ExampleCheckLogin : IExamplesProvider<CheckLoginExample>
    {
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckLoginExample))]
        public CheckLoginExample GetExamples()
        {
            return new CheckLoginExample
            {
                userMessage = "Login Success",
                LoginID = "admin",
                password = "$2a$11$5IqEzHK0d1F16UK2Sk2H6OxfjsPel6vElJl2s7OC0TcnB5aOptEVy",
                userToken = "eyJhbGciOiJIUzI1NiJ9.ew0KICAic3ViIjogInRlcyNAMTEydzEyMzI1Njc4OXFlc2ZkZXRndnF3ZWF4Y3Zibm11bGlsdXkpKComXiIsDQogICJuYW1lIjogImFobWFkIGthc3dhcmkiLA0KICAiaWF0IjogMDQvMDMvMjAyNCAwOToxOSBva3RvYmVyIDEyMg0KfQ.29mJbzwy-m6ER6NVcb4HFdlYvYF9yfCZ7PEsMz0Qmrg",
            };
        }
    }
}
