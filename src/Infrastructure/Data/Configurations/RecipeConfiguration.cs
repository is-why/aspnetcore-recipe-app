using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(e => e.Id);  // 业务主键
        builder.Property(e => e.Id).HasMaxLength(13);  // R + 8位日期 + 4位序列号
        builder.Property(e => e.Title).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(500);
        
        builder.HasMany(e => e.Ingredients)
            .WithOne()
            .HasForeignKey(e => e.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(e => e.Steps)
            .WithOne()
            .HasForeignKey(e => e.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(e => e.Tags)
            .WithMany(e => e.Recipes);
    }
}
