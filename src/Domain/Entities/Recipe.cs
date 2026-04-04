namespace Domain.Entities;

public class Recipe
{
    public string Id { get; set; } = string.Empty;  // 业务主键，如R202604040001
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }        // 最大500字符
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public ICollection<Step> Steps { get; set; } = new List<Step>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();  // 多对多
}