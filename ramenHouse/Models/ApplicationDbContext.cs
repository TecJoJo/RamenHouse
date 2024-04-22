using Microsoft.EntityFrameworkCore;

namespace ramenHouse.Models
{
   
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            public DbSet<User> Customers { get; set; }
            



        }
    
}
