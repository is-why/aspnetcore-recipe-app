# AGENTS.md - Agent Coding Guidelines

## Project Overview
ASP.NET Core 8 recipe management app using Clean Architecture with SQLite and EF Core.

## Project Structure
```
src/
├── Domain/                    # Core business entities
│   └── Entities/              # Entity classes
├── Application/              # Business logic
│   ├── Interfaces/            # Interface definitions
│   ├── DTOs/                  # Data transfer objects
│   ├── Services/              # Service implementations
│   └── Validation/            # Validation logic
├── Infrastructure/            # Data access
│   ├── Data/                  # DbContext, configurations, seed data
│   ├── Repositories/          # Repository implementations
│   └── Services/              # External services
└── API/                       # HTTP endpoints
    └── Controllers/           # Controllers
```

---

## Build Commands

### Build solution and projects
```bash
dotnet build
dotnet build src/API/API.csproj
dotnet build -c Release
```

### Run API
```bash
dotnet run --project src/API/API.csproj
```

### EF Migrations
```bash
dotnet ef migrations add <Name> --project src/Infrastructure/Infrastructure.csproj --startup-project src/API/API.csproj
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/API/API.csproj
```

---

## Testing Commands

### Create test project
```bash
dotnet new xunit -n Tests.Domain -o tests/Domain.Tests
dotnet sln add tests/Domain.Tests/Domain.Tests.csproj
```

### Run tests
```bash
dotnet test
dotnet test tests/Domain.Tests/Domain.Tests.csproj
dotnet test --filter "FullyQualifiedName~Namespace.TestClassName.TestMethodName"
dotnet test -v n
```

---

## Code Style Guidelines

### General
- Target: .NET 8.0, C# 12, ImplicitUsings: enable, Nullable: enable
- Use file-scoped namespaces: `namespace Domain.Entities;`
- Group using statements: System > Third-party > Project-specific

### Naming Conventions
| Element | Convention | Example |
|---------|------------|---------|
| Classes/Interfaces | PascalCase | `RecipeService`, `IRecipeRepository` |
| Methods | PascalCase | `GetAllRecipesAsync()` |
| Properties | PascalCase | `RecipeTitle`, `CreatedDate` |
| Private fields | _camelCase | `_logger`, `_repository` |
| Parameters/local | camelCase | `recipeId`, `createDto` |

### Types
- Use `record` for DTOs/immutable types, `class` for entities/services
- Use `interface` for abstractions
- Prefer `var` for local variables; explicit types for public APIs
- Collections as `ICollection<T>` with default values

```csharp
public class Recipe
{
    public string Id { get; set; } = string.Empty;  // 业务主键，如"R202604040001"
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public ICollection<Step> Steps { get; set; } = new List<Step>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
```

### Async/Await
- Use for I/O; name with `Async` suffix; return `Task<T>`

```csharp
public async Task<RecipeDto> GetRecipeByIdAsync(string id)
    => await _repository.GetByIdAsync(id);
```

### Error Handling
- Try-catch for operations that may fail
- Return appropriate HTTP status codes

```csharp
try {
    var recipe = await _service.GetByIdAsync(id);
    return recipe is null ? NotFound() : Ok(recipe);
}
catch (Exception ex) {
    _logger.LogError(ex, "Error");
    return StatusCode(500);
}
```

### Dependency Injection
- Inject via constructor; register in `Program.cs`

```csharp
public class RecipeService(IRecipeRepository _repository, ILogger<RecipeService> _logger) 
    : IRecipeService
{
}
```

### Controllers
- Use `[ApiController]`, `[Route("api/[controller]")]`
- Inject logger and services via constructor

```csharp
[ApiController][Route("api/[controller]")]
public class RecipesController(ILogger<RecipesController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        => Ok(await _service.GetAllAsync());
}
```

### Domain/EF Core
- Entities as POCOs with navigation properties
- Use Fluent API for configuration
- DbContext in Infrastructure layer with repository pattern

---

## Important Notes
- **No tests** - Test projects need to be created
- **No linter** - Consider adding EditorConfig or StyleCop
- **SQLite** - Database persistence
- **Swagger** - Available at `/swagger` in development

## Entity Design
- Recipe.Id 和 Tag.Id 为业务主键，由程序生成，格式为"前缀+8位日期+4位序列号"（如 R202604040001、T202604040001）
- Tag 与 Recipe 为多对多关系，Recipe.ICollection\<Tag\> + Tag.ICollection\<Recipe\>，EF自动生成中间表
- Ingredient 和 Step 使用复合主键 (RecipeId + RowNo)
