using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.ViewModels
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
    }
}
