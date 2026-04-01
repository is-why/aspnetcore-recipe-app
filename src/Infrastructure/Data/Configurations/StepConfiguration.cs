using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(EntityTypeBuilder<Step> builder)
    {
        builder.HasKey(e => new { e.RecipeId, e.RowNo });  // 复合主键
        builder.Property(e => e.RecipeId).HasMaxLength(6);
        builder.Property(e => e.Content).IsRequired();
    }
}
