using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIServer.Data;
using APIServer.Filters;
using APIServer.Models;
using APIServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace APIServer.Controllers.Api
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ContentController : ControllerBase
    {

        private readonly APIServerContext _context;
        private readonly string apiControllerName = typeof(ContentController).Name.Substring(0, typeof(ContentController).Name.Length - 10);

        public ContentController(APIServerContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Content>>> Query([FromQuery] ContentParameters qparams)
        {
            List<Content> contents = new List<Content>();

            // Filter the request as per the query parameters
            if (!string.IsNullOrEmpty(qparams.Ids)){
                
                // Use the ids param to filter for requested content ids
                string [] ids = qparams.Ids.Split(",");

                foreach (string stringId in ids) {
                    if (Int32.TryParse(stringId, out int id)) {
                        if (id == 1) {
                            contents.Add(new Content {
                                Id = id,
                                Title = "Welcome to our \"Live Well\" community",
                                Body = "Our vision is to use an integrated approach to address our population's health and wellness. The Healthy Life Course approach works toward building lasting, improved health and wellness for our people. We support our community based on this approach, which suggests that the health and wellness outcomes of individuals, families, and communities depend on many variables, including universality, equity and community health variables. These can include things that can improve our wellbeing (protective factors) and things that can worsen it (risk factors) throughout life. The model also provides for a broader understanding of the population's health and health service delivery, which is key in pursuing universal health for the current and future populations of the Region.You are welcome to join our \"Live Well\" initiative!"
                            });
                        } else if (id == 2) {
                            contents.Add(new Content
                            {
                                Id = id,
                                Image = "/static/images/PopulationHealthAndWellness_Gray.png"
                            });
                        } else if (id == 3) {
                            contents.Add(new Content
                            {
                                Id = id,
                                Title = "USERNAME",
                                Body = "Please verify your username"
                            });
                        } else if (id == 4) {
                            contents.Add(new Content
                            {
                                Id = id,
                                Title = "PASSWORD",
                                Body = "Please verify your password"
                            });
                        } else if (id == 5) {
                            contents.Add(new Content
                            {
                                Id = id,
                                Body = "Your session has expired, please login again"
                            });
                        } else if (id == 6) {
                            contents.Add(new Content
                            {
                                Id = id,
                                Title = "Login"
                            });
                        }
                    }
                }
            }

            return contents;
        }

        [HttpGet("{dependency_id:int}")]
        public async Task<ActionResult<Content>> Query([FromRoute] int dependency_id, [FromQuery] _basisParameters qparams)
        {
            Content content = new Content();

            // Filter the request as per the query parameters
            if (dependency_id == 1) {
                content.Id = 1;
                content.Title = "Welcome to our \"Live Well\" community";
                content.Title = "Our vision is to use an integrated approach to address our population's health and wellness. The Healthy Life Course approach works toward building lasting, improved health and wellness for our people. We support our community based on this approach, which suggests that the health and wellness outcomes of individuals, families, and communities depend on many variables, including universality, equity and community health variables. These can include things that can improve our wellbeing (protective factors) and things that can worsen it (risk factors) throughout life. The model also provides for a broader understanding of the population's health and health service delivery, which is key in pursuing universal health for the current and future populations of the Region.You are welcome to join our \"Live Well\" initiative!";
                content.Image =  "/static/images/PopulationHealthAndWellness_Gray.png";
            }

            return content;
        }
    }
}
