using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIServer.Data;
using APIServer.Filters;
using APIServer.Models;
using APIServer.Models.Basis;
using APIServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APIServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly APIServerContext _context;
        private readonly IConfiguration _config;
        private readonly string apiControllerName = typeof(AuthenticationController).Name.Substring(0, typeof(AuthenticationController).Name.Length - 10);

        public AuthenticationController(APIServerContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {

            return GenericError.create(this, StatusCodes.Status404NotFound,
                                        "Authentication requests should be sent to ~/" + apiControllerName + "/authenticate");
        }

        [Route("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> Refresh() {

            return GenericError.create(this, StatusCodes.Status405MethodNotAllowed, "Not implemented.");
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginUser user)
        {
            // TODO: 
            // 1) Save refresh token to db
            // 2) Delete old refresh tokens from db
            // 3) Add refresh-token, revoke token and logout routes 

            // Find an existing consumer.
            var userInDB = String.IsNullOrEmpty(user?.UserName) ? null 
                            : await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == user.UserName);

            // Create a token
            if (userInDB != null)
            {
                var authResponse = Helpers.AuthenticationHelper.LoginResponse(_config, 
                                                                                user, 
                                                                                userInDB, 
                                                                                HttpContext.Connection.RemoteIpAddress.ToString());
                if (authResponse != null)
                {
                    //Return signed JWT token
                    return Ok(authResponse);
                }
            }

            return GenericError.create(this, StatusCodes.Status401Unauthorized, 
                                        "Could not authenticate the provided user. Please check your credentials.");
        }

        [HttpGet("silent")]
        public async Task<IActionResult> Silent()
        {
            var canAccess = Helpers.AuthenticationHelper.canAccessUserData(HttpContext.User);
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            string username = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            // Find an existing consumer.
            var userInDB = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == username);

            // Create a token
            if (userInDB != null)
            {
                var authResponse = Helpers.AuthenticationHelper.LoginResponse(_config,
                                                                                userInDB,
                                                                                HttpContext.Connection.RemoteIpAddress.ToString());
                if (authResponse != null)
                {
                    //Return signed JWT token
                    return Ok(authResponse);
                }
            }

            return GenericError.create(this, StatusCodes.Status401Unauthorized,
                                        "Could not authenticate the provided user. Please check your credentials.");
        }


    }
}
