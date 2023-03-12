using A3_BusinessGroupProjectApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace A3_BusinessGroupProjectApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BusinessGroupProject;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

        public DbSet<Immunization> Immunizations { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
