using SimplyCrudAPI.Data;
using SimplyCrudAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using SimplyCrudAPI.ExampleData.UserData;

namespace SimplyCrudAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserAPIDbContext _dbContext;

        public UserController(UserAPIDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        //untuk register user controller. ubah disini untuk register user
        /// <summary>
        /// Register User for create account to generate token on Check Login.
        /// </summary>
        /// <response code="200">This API is used to register users. The input data below is an example of the values ​​that will be stored.</response>
        [HttpPost("RegisterUser")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserProfileRegisterBad), 400)]
        [ProducesResponseType(typeof(UserProfileRegisterData), 200)]
        public async Task<IActionResult> AddUserRegister(UserProfileRegister userProfile)
        {
            try
            {
                if (userProfile == null ||
                    string.IsNullOrEmpty(userProfile.LoginID) ||
                    string.IsNullOrEmpty(userProfile.Password) ||
                    string.IsNullOrEmpty(userProfile.Email) ||
                    string.IsNullOrEmpty(userProfile.Address) ||
                    userProfile.Gender == null ||
                    userProfile.Age == null)
                {
                    return BadRequest(new
                    {
                        message = "All fields are required and cannot be null."
                    });
                }

                string salt = "$2a$11$5IqEzHK0d1F16UK2Sk2H6O";

                var userProfileRegister = new UserProfile()
                {
                    LoginID = userProfile.LoginID,
                    Password = BCrypt.Net.BCrypt.HashPassword(userProfile.Password, salt),
                    Email = userProfile.Email,
                    Address = userProfile.Address,
                    Gender = userProfile.Gender,
                    Age = userProfile.Age,

                };
                await _dbContext.UserProfiles.AddAsync(userProfileRegister);
                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    message = "User registered successfully",
                    userProfile = userProfileRegister
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        //untuk register check login. ubah disini untuk check login user
        /// <summary>
        /// Check Login for getting token Authorization.
        /// </summary>
        /// <response code="200">Use the registered logId and password.</response>
        /// <response code="404">User logId and Password Not Found. API will give response "Login Failed"</response>
        [Route("CheckLogin")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckLoginExample))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CheckLoginExampleNotFound))]
        public async Task<IActionResult> CheckLogin(UserLogin userLogin)
        {
            try
            {
                var userMessage = "Login Failed";
                var user = await _dbContext.UserProfiles
                .Where(u => u.LoginID == userLogin.LoginID)
                .Select(u => new { u.Password })
                .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Unauthorized(new
                    {
                        UserMessage = userMessage
                    });
                }

                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);


                if (isPasswordCorrect)
                {
                    userMessage = "Login Success";
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddSeconds(30),
                        signingCredentials: signIn);
                    var expires = token.ValidTo.ToLocalTime();
                    var expired = DateTime.Now.AddSeconds(30);

                    Console.WriteLine(expires);
                    Console.WriteLine(expired);

                    var logCheckLogin = new LogCheckLogin
                    {
                        userMessage = userMessage,
                        userToken = new JwtSecurityTokenHandler().WriteToken(token),
                        LoginID = userLogin.LoginID,
                        password = user.Password,
                        ExpiredDate = expires,
                    };

                    await _dbContext.LogCheckLogins.AddAsync(logCheckLogin);
                    await _dbContext.SaveChangesAsync();

                    return Ok(new
                    {
                        UserMessage = userMessage,
                        UserToken = new JwtSecurityTokenHandler().WriteToken(token),
                        UserProfile = user
                    });

                }
                else
                {
                    return Unauthorized(new
                    {
                        UserMessage = userMessage
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
