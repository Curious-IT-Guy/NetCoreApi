using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Models;


namespace NetCoreApi.Data.Entities.Schemes
{
    public class TodoStateEntity : IEntityTypeConfiguration<TodoState>
    {
        public void Configure(EntityTypeBuilder<TodoState> entity)
        {
            entity.Property(e => e.Id)
                        .IsRequired();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasData(
                new { Id = 1, Title = "Pending" },
                new { Id = 2, Title = "Ongoing" },
                new { Id = 3, Title = "Done" }
             );
        }
    }
}