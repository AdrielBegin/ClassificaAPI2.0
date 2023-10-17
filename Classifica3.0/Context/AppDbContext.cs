using Classifica3._0.Model;
using Microsoft.EntityFrameworkCore;

namespace Classifica3._0.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
    }
}
