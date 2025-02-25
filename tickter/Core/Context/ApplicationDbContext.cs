using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using tickter.Core.Entities;

namespace tickter.Core.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
