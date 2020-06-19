using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIServer.Data;
using APIServer.Filters;
using APIServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIServer.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly APIServerContext _context;
        private readonly string apiControllerName = typeof(UserController).Name.Substring(0, typeof(UserController).Name.Length - 10);

        public UserController(APIServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Query([FromQuery] _basisParameters qparams)
        {
            var canAccess = Helpers.AuthenticationHelper.canAccessUserData(HttpContext.User);
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            string username = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var users = await _context.Users.Where(x => !String.IsNullOrEmpty(username) && x.UserName == username).AsNoTracking().ToListAsync();

            // Filter the request as per the query parameters
            return users;
        }
    }
}
