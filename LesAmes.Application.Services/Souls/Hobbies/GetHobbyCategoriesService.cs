using LesAmes.Dto.Souls.Hobbies;
using LesAmes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Application.Services.Souls.HobbyCategories;

public class GetHobbyCategoriesService
{
    private readonly AppDbContext _dbContext;

    public GetHobbyCategoriesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HobbyCategoryOutput> GetHobbyCategoriesAsync()
    {
        var hobbyCategories = await _dbContext.HobbyCategories
             .Include(hc => hc.Hobbies)
             .ToListAsync();

        return new HobbyCategoryOutput
        {
            HobbyCategories = hobbyCategories.Select(hc => new HobbyCategoryDto
            {
                Id = hc.Id,
                Name = hc.Name,
                Hobbies = hc.Hobbies.Select(h => new HobbyDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    Description = h.Description
                }).ToList()
            }).ToList()
        };
    }
}
