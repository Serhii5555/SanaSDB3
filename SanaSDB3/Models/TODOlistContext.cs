using Microsoft.EntityFrameworkCore;

namespace SanaSDB3
{
    public class TODOlistContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public TODOlistContext(DbContextOptions<TODOlistContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Визначення зв'язку багато-до-одного між Task і Category
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
