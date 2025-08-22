using LesAmes.Dto.Souls.AgeRanges;
using LesAmes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Application.Services.Souls.AgeRanges;
public class GetAgeRangeService
{
    private readonly AppDbContext _dbContext;

    public GetAgeRangeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAgeRangeOutput> GetAgeRangeAsync()
    {
        var agesRanges = await _dbContext
            .AgeRanges
            .OrderBy(ar => ar.IsActive)
            .ThenBy(ar => ar.AgeMin)
            .ToListAsync();

        return new GetAgeRangeOutput
        {
            AgeRanges = agesRanges.Select(ar => new AgeRangeDto
            {
                Id = ar.Id,
                AgeMin = ar.AgeMin,
                AgeMax = ar.AgeMax,
                IsActive = ar.IsActive
            }).ToList()
        };
    }
}
