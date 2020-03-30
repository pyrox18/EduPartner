using EduPartner.MvcApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EduPartner.MvcApp.Data
{
    public class EduPartnerDbContext : DbContext
    {
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public EduPartnerDbContext(DbContextOptions<EduPartnerDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Parent>()
                .HasData(new Parent
                {
                    Id = Guid.NewGuid(),
                    Name = "Ali bin Abu",
                    Email = "ali@test.com",
                    Password = "abc123",
                    Address = "1, Jalan 2, 40000 Shah Alam, Selangor"
                });
        }
    }
}
