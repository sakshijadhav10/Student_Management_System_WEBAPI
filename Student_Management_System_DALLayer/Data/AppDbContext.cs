using Microsoft.EntityFrameworkCore;
using Student_Management_System_DAL.Models;

namespace Student_Management_System_DAL.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
