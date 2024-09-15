using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class MyContext :IdentityDbContext<IdentityUser>
    {
        public MyContext() : base()
        {
        }
        public MyContext(DbContextOptions dbContextOptions):base(dbContextOptions) {
        
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Identity_MVC;Integrated Security=True;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Identity.Models.LoginModel> LoginModel { get; set; } = default!;
        public DbSet<Identity.Models.RegisterModel> RegisterModel { get; set; } = default!;
    }
}
