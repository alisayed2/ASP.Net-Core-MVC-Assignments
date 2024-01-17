using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC.Models;

namespace MVC.Entities
{
    public class ITIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Avaialble only with Core
            //optionsBuilder.UseSqlServer("Server = .; Database=MinaiMVC ;Trust-Connection = true ; Encrypt = False");
            //avaliable with Framework and Core
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ITIMVC;Integrated Security=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }
        public virtual DbSet<CrsResult> CrsResults { get; set; }
    }
}
