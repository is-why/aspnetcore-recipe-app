using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasMaxLength(13);  // T + 8位日期 + 4位序列号
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(e => e.Name).IsUnique();
    }
}
