using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NetCoreApi.Data.Models;


namespace NetCoreApi.Data.Entities.Schemes
{
    public class UserEntity : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id)
                        .IsRequired();

            entity.Property(e => e.Firstname)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.Lastname)
                .HasMaxLength(30);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.HasMany(e => e.Todo)
                .WithOne();
        }
    }
}