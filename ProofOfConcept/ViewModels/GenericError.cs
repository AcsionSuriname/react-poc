using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models.Basis
{
    public class GenericError
    {

        public static ObjectResult create(ControllerBase controller, int status, string detail)
        {
            var error = new GenericError(status, detail);
            return controller.StatusCode(status, error);
        }

        public GenericError(int status, string detail)
        {
            this.status = status;
            switch (status)
            {
                case StatusCodes.Status400BadRequest:
                    this.title = "Bad Request";
                    break;
                case StatusCodes.Status401Unauthorized:
                    this.title = "Unauthorized Access";
                    break;
                case StatusCodes.Status403Forbidden:
                    this.title = "Access Forbidden";
                    break;
                case StatusCodes.Status404NotFound:
                    this.title = "Resource Not Found";
                    break;
            }
            this.title = title;
            this.errors = new string[] { detail };
        }

        public int status { get; set; }
        public string title { get; set; }
        public string[] errors { get; set; }
    }
}
