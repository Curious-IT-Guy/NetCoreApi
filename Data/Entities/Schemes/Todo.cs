using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Models;


namespace NetCoreApi.Data.Entities.Schemes
{
    public class TodoEntity : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> entity)
        {
            entity.Property(e => e.Id)
                        .IsRequired();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CompletedAt)
            .HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(e => e.TodoState);
        }
    }
}