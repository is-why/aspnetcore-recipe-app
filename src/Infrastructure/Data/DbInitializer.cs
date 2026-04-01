using Domain.Entities;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Recipes.Any()) return;

        var tags = new[]
        {
            new Tag { Id = "000001", Name = "家常菜" },
            new Tag { Id = "000002", Name = "快手菜" },
            new Tag { Id = "000003", Name = "甜品" },
        };
        context.Tags.AddRange(tags);

        var recipes = new[]
        {
            new Recipe
            {
                Id = "000001",
                Title = "番茄炒蛋",
                Description = "简单美味的家常菜",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Tags = new List<Tag> { tags[0], tags[1] },
                Ingredients = new List<Ingredient>
                {
                    new() { RecipeId = "000001", RowNo = 1, Name = "番茄", Quantity = "2个" },
                    new() { RecipeId = "000001", RowNo = 2, Name = "鸡蛋", Quantity = "3个" },
                },
                Steps = new List<Step>
                {
                    new() { RecipeId = "000001", RowNo = 1, Content = "番茄切块，鸡蛋打散" },
                    new() { RecipeId = "000001", RowNo = 2, Content = "炒蛋至半熟，加入番茄翻炒" },
                },
            },
            new Recipe
            {
                Id = "000002",
                Title = "蛋挞",
                Description = "简单易做的甜品",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Tags = new List<Tag> { tags[2] },
                Ingredients = new List<Ingredient>
                {
                    new() { RecipeId = "000002", RowNo = 1, Name = "蛋挞皮", Quantity = "6个" },
                    new() { RecipeId = "000002", RowNo = 2, Name = "鸡蛋", Quantity = "2个" },
                },
                Steps = new List<Step>
                {
                    new() { RecipeId = "000002", RowNo = 1, Content = "鸡蛋打匀加入牛奶和糖" },
                    new() { RecipeId = "000002", RowNo = 2, Content = "倒入蛋挞皮，放入烤箱" },
                },
            },
        };
        context.Recipes.AddRange(recipes);

        context.SaveChanges();
    }
}
