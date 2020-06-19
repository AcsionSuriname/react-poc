using APIServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.ViewModels
{
    public class MessagesResponse
    {
        public int Count { get; set; }
        public int UnreadCount { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
