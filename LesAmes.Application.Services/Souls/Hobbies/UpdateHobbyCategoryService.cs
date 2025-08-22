using LesAmes.Domain.Hobbies;
using LesAmes.Dto.Souls.Hobbies;
using LesAmes.Infrastructure.Database;

namespace LesAmes.Application.Services.Souls.HobbyCategories;

public class UpdateHobbyCategoryService
{
    private readonly AppDbContext _dbContext;

    public UpdateHobbyCategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateHobbyCategoryAsync(string id, HobbyCategoryInput input)
    {
        var hobbyCategory = await _dbContext.HobbyCategories.FindAsync(id);
        if (hobbyCategory == null)
        {
            throw new KeyNotFoundException($"Hobby category with ID {id} not found.");
        }

        hobbyCategory.Name = input.Name;
        _dbContext.HobbyCategories.Update(hobbyCategory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddHobbyAsync(string categoryId, HobbyInput input)
    {
        var hobbyCategory = await _dbContext.HobbyCategories.FindAsync(categoryId);
        if (hobbyCategory == null)
        {
            throw new KeyNotFoundException($"Hobby category with ID {categoryId} not found.");
        }
        var hobby = new Hobby
        {
            Name = input.Name,
            Description = input.Description,
            HobbyCategoryId = categoryId
        };
        _dbContext.Hobbies.Add(hobby);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateHobbyAsync(string hobbyId, HobbyInput input)
    {
        var hobby = await _dbContext.Hobbies.FindAsync(hobbyId);
        if (hobby == null)
        {
            throw new KeyNotFoundException($"Hobby with ID {hobbyId} not found.");
        }
        hobby.Name = input.Name;
        hobby.Description = input.Description;
        _dbContext.Hobbies.Update(hobby);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteHobbyAsync(string hobbyId)
    {
        var hobby = await _dbContext.Hobbies.FindAsync(hobbyId);
        if (hobby == null)
        {
            throw new KeyNotFoundException($"Hobby with ID {hobbyId} not found.");
        }
        _dbContext.Hobbies.Remove(hobby);
        await _dbContext.SaveChangesAsync();
    }
}
