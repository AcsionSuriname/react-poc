using APIServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Data
{
    public class APIServerContext : DbContext
    {
        public APIServerContext(DbContextOptions<APIServerContext> options)
            : base(options)
        { }

        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }
        public DbSet<MessageType> MessageTypes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }

    }
}