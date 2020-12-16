using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Entities.Schemes;
using NetCoreApi.Data.Models;

namespace NetCoreApi.Data.Entities
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Todo> Todo { get; set; }
        public virtual DbSet<TodoState> TodoState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntity());
            modelBuilder.ApplyConfiguration(new TodoEntity());
            modelBuilder.ApplyConfiguration(new TodoStateEntity());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
