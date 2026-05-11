using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.Library.Models;
namespace Stallstjarnornas.WebAPI.Data
{
    public class StallstjarnonasDbContext : DbContext
    {
        StallstjarnonasDbContext(DbContextOptions<StallstjarnonasDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<MailLog> MailLogs { get; set; }

        public DbSet<OpeningDay> OpeningDays { get; set; }

        public DbSet<Sitting> Sittings { get; set; }

    }
}
