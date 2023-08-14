using Asp.net_School.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_School.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base (options)
        {
        }
        public DbSet<Student> students { get; set; }
    }
}
