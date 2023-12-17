using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShareMate.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace ShareMate.DbContext
{


    public class DbContextApplication : IdentityDbContext<User>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Enroll> Enrolls { get; set; }
        public DbSet<Student> Students { get; set; } 
        public DbSet<User> Users { get; set; } 

        public DbContextApplication(DbContextOptions<DbContextApplication> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }

        // Define a method for the stored procedure
        public IEnumerable<Course> SearchCourses(string searchTerm)
        {
            return Courses.FromSqlRaw("EXEC SearchCourses @p0", searchTerm).ToList();
        }
    }


}
