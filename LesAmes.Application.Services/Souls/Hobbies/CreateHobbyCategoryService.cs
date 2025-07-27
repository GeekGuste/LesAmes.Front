using LesAmes.Domain.Hobbies;
using LesAmes.Dto.Souls.Hobbies;
using LesAmes.Infrastructure.Database;

namespace LesAmes.Application.Services.Souls.Hobbies;

public class CreateHobbyCategoryService
{
    private readonly AppDbContext _dbContext;

    public CreateHobbyCategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateHobbyCategoryAsync(HobbyCategoryInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
        {
            throw new ArgumentException("Category name cannot be empty.", nameof(input.Name));
        }
        var hobbyCategory = new HobbyCategory
        {
            Name = input.Name
        };
        _dbContext.HobbyCategories.Add(hobbyCategory);
        await _dbContext.SaveChangesAsync();
    }
}
