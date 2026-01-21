using Microsoft.EntityFrameworkCore;
using Herb_Track_Bulgaria_Server.Models;

namespace Herb_Track_Bulgaria_Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Herb> Herbs { get; set; }
    }
}