namespace Domain.Entities;

public class Ingredient
{
    public string RecipeId { get; set; } = string.Empty;  // 外键
    public int RowNo { get; set; }                          // 排序号，从1开始
    public string Name { get; set; } = string.Empty;       // 原料名称
    public string Quantity { get; set; } = string.Empty;   // 用量，如500g
}