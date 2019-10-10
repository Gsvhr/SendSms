using Microsoft.EntityFrameworkCore;
using SendSms.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendSms.EntityFramework
{
    public class SendSmsContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }
        public DbSet<Message> Messages { get; set; }

        public SendSmsContext() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sms.db");
    }
}
