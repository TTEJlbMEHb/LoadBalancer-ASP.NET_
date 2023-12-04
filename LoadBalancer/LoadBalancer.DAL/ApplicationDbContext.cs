using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Enum;
using LoadBalancer.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Calculator> Calculator { get; set; }
        public DbSet<Matrix> Matrix { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User
                {
                    Id = 1.ToString(),
                    Username = "Admin",
                    Email = "vlad.linnik555@gmail.com",
                    Password = HashPasswordHelper.HashPassword("adminvlad"),
                    Role = Role.Admin,
                    HeavyTasks = new List<Matrix>()
                });

                builder.ToTable("Users").HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
