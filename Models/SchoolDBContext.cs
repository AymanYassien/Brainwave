using Microsoft.EntityFrameworkCore;

namespace webIti.Models;

public class SchoolDBContext : DbContext
{

    public SchoolDBContext() : base()
    {
        
    }

    public SchoolDBContext(DbContextOptions options) :base(options)
    {
        
    }
    public DbSet<CourseResult> Results { get; set; }
    public DbSet<Trainee> Trainees { get; set; } // prop
    public DbSet<instructor> Instructors { get; set; } // prop
    public DbSet<Course> Courses { get; set; } // prop
    public DbSet<newDepartment> Departments { get; set; } // prop


    // override funcname   = = quick
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=instuate;" +
            "User=sa;Password=YourStrongPassword123;TrustServerCertificate=true");
        base.OnConfiguring(optionsBuilder);
    }
}