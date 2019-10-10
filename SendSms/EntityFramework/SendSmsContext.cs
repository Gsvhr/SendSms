using Microsoft.EntityFrameworkCore;
using SendSms.Core.Models;

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
