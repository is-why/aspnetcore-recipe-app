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
├── Domain/                    # 领域层：核心业务实体
│   └── Entities/              # 实体类
├── Application/               # 应用层：业务逻辑
│   ├── Interfaces/            # 接口定义
│   ├── DTOs/                  # 数据传输对象
│   ├── Services/              # 服务实现
│   └── Validation/            # 验证逻辑
├── Infrastructure/            # 基础设施层：数据访问
│   ├── Data/                  # DbContext、配置、种子数据
│   ├── Repositories/          # 仓储实现
│   └── Services/              # 外部服务
└── API/                       # 表现层：HTTP接口
    └── Controllers/           # 控制器
```

## 实体

### Recipe
| 字段 | 类型 | 说明 |
| :--- | :--- | :--- |
| Id | string | 业务主键，如 "R202604040001"（前缀R + 8位日期 + 4位序列号） |
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
| Id | string | 业务主键，如 "T202604040001"（前缀T + 8位日期 + 4位序列号） |
| Name | string | 标签名称，唯一 |
| Recipes | ICollection\<Recipe\> | 食谱列表，多对多，EF自动生成中间表 |
