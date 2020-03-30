using EduPartner.MvcApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EduPartner.MvcApp.Data
{
    public class EduPartnerDbContext : DbContext
    {
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
