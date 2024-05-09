using Microsoft.EntityFrameworkCore;

namespace ramenHouse.Models
{
   
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            public DbSet<User> Users { get; set; }
            
            public DbSet<Meal> Meals { get; set; }

            public DbSet<Allergy> Allergies { get; set; }

        public DbSet<Category> Categories { get; set; }


        }
    
}
