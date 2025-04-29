using Cursos.Domain.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Cursos.Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<KnowledgePlatform> KnowledgePlatforms { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<KnowledgePlatform>().Property(e => e.CreatedDate).HasColumnType("datetime");
        //    modelBuilder.Entity<Instructor>().Property(p => p.CreatedDate).HasColumnType("datetime");
        //    modelBuilder.Entity<Course>().Property(c => c.CreatedDate).HasColumnType("datetime");
        //}
    }
}
