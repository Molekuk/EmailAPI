using Microsoft.EntityFrameworkCore;

namespace EmailAPI.Models
{
    public class EmailContext : DbContext
    {
        public DbSet<SentMessage> Emails { get; set; }
        public EmailContext(DbContextOptions<EmailContext> options) : base(options) { }

    }
}
