using DesenvWebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace DesenvWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<SubjectCurriculum> SubjectCurriculums { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curriculum>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Subject>()
                .HasIndex(c => c.Code)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
