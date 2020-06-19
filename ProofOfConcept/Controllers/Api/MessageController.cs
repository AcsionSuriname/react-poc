using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIServer.Data;
using APIServer.Filters;
using APIServer.Models;
using APIServer.Models.Basis;
using APIServer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIServer.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly APIServerContext _context;
        private readonly string apiControllerName = typeof(MessageController).Name.Substring(0, typeof(MessageController).Name.Length - 10);

        public MessageController(APIServerContext context)
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

        [HttpGet("{user_id:guid}")]
        public async Task<ActionResult<MessagesResponse>> ByUserID([FromRoute] Guid user_id, [FromQuery] _basisParameters qparams)
        {
            var canAccess = Helpers.AuthenticationHelper.canAccessUserData(HttpContext.User);
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            string username = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string userGID = user_id.ToString();

            if (!canAccess) {
                canAccess =  _context.Users.Any(x => !String.IsNullOrEmpty(username) && x.UserName == username && x.UserGID == userGID);
            }

            if (canAccess) {
                // Get the messages associated with this userGID
                var messageCount = _context.Messages.Count(x => x.MessageRecipients.Any(y => y.UserGID == userGID));
                var unreadMessageCount = _context.Messages.Count(x => x.MessageRecipients.Any(y => y.UserGID == userGID && !y.MessageRead));

                var messages = await _context.Messages.Where(x => x.MessageRecipients.Any(y => y.UserGID == userGID)).Take(20).AsNoTracking().ToListAsync();

                // Send the response back
                return new MessagesResponse
                {
                    Count = messageCount,
                    UnreadCount = unreadMessageCount,
                    Messages = messages
                };
            }

            // If the user is unknown, reply with an error
            return GenericError.create(this, StatusCodes.Status401Unauthorized,
                                        "You do not have access to this user's messages");
        }
    }
}
