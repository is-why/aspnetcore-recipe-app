namespace Domain.Entities;

public class Tag
{
    public string Id { get; set; } = string.Empty;  // 业务主键，6位数字如"000001"
    public string Name { get; set; } = string.Empty; // 唯一
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();  // 多对多，EF自动生成中间表
}
