# aspnetcore-recipe-app

基于 ASP.NET Core 8 的食谱管理应用。

## 技术栈

- ASP.NET Core 8
- SQLite
- EF Core

## 快速开始

```bash
# 编译
dotnet build

# 运行
dotnet run --project src/API/API.csproj
```

## 项目结构

```
src/
├── Domain/          # 领域实体
├── Application/     # 业务逻辑、接口、DTO
├── Infrastructure/  # 数据访问、仓储
└── API/             # Web API
```

## 实体

### Recipe
| 字段 | 类型 | 说明 |
| :--- | :--- | :--- |
| Id | string | 业务主键，6位数字，如 "000001"、"000002" |
| Title | string | 食谱标题，必填 |
| Description | string?(500) | 食谱简介，可为空，最大500字符 |
| CreatedAt | DateTime | 创建时间 |
| UpdatedAt | DateTime | 更新时间 |
| Ingredients | ICollection\<Ingredient\> | 原料列表，导航属性 |
| Steps | ICollection\<Step\> | 制作步骤列表，导航属性 |
| Tags | ICollection\<Tag\> | 标签列表，多对多，EF自动生成中间表 |

### Ingredient (复合主键: RecipeId + RowNo)
| 字段 | 类型 | 说明 |
| :--- | :--- | :--- |
| RecipeId | string | 所属食谱ID，外键 |
| RowNo | int | 排序号，同一食谱内从1开始 |
| Name | string | 原料名称 |
| Quantity | string | 用量，如"500g"、"2个" |

### Step (复合主键: RecipeId + RowNo)
| 字段 | 类型 | 说明 |
| :--- | :--- | :--- |
| RecipeId | string | 所属食谱ID，外键 |
| RowNo | int | 排序号，同一食谱内从1开始 |
| Content | string | 步骤描述文字 |

### Tag
| 字段 | 类型 | 说明 |
| :--- | :--- | :--- |
| Id | string | 业务主键，6位数字，如 "000001"、"000002" |
| Name | string | 标签名称，唯一 |
| Recipes | ICollection\<Recipe\> | 食谱列表，多对多，EF自动生成中间表 |
