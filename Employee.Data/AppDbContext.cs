using Employees.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }
        //public AppDbContext()
        //{
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmployeeDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
