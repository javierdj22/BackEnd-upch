using Microsoft.EntityFrameworkCore;
using BackEndUpch.Domain;

namespace BackEndUpch.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; } // plural es lo usual
    }
}
