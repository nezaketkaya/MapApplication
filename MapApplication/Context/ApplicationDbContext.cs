using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MapApplication.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Point> points { get; set; }
    }
}
