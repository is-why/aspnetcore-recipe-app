namespace Domain.Entities;

public class Step
{
    public string RecipeId { get; set; } = string.Empty;  // 外键
    public int RowNo { get; set; }                          // 排序号，从1开始
    public string Content { get; set; } = string.Empty;     // 步骤描述
}
