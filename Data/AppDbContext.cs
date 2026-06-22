using FormAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace FormAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<FormPerson> FormPeople { get; set; }
    }
}
