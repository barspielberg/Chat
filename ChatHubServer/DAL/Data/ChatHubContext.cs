using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace DAL.Data
{
    public class ChatHubContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public ChatHubContext(DbContextOptions<ChatHubContext> options) : base(options)
        {
            //create the database if not exist
            //for the tester convenience :)
            string path = "c:\\temp";
            Directory.CreateDirectory(path);
            Database.Migrate();
        }
       

    }
}
