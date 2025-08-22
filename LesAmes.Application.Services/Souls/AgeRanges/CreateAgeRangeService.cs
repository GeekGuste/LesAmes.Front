using LesAmes.Dto.Souls.AgeRanges;
using LesAmes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Application.Services.Souls.AgeRanges;

public class CreateAgeRangeService
{
    private readonly AppDbContext _dbContext;

    public CreateAgeRangeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAgeRangeAsync(AgeRangeInput input)
    {
        if (input.AgeMin < 0 || input.AgeMax < 0 || input.AgeMin >= input.AgeMax)
        {
            throw new ArgumentException("Invalid age range specified.");
        }

        var existantAgeRange = await _dbContext.AgeRanges.Where(ar => ar.AgeMin == input.AgeMin && ar.AgeMax == input.AgeMax).FirstOrDefaultAsync();
        if (existantAgeRange != null)
        {
            throw new InvalidOperationException("Cette tranche d'age existe déjà.");
        }

        var ageRange = new Domain.Souls.AgeRange
        {
            AgeMin = input.AgeMin,
            AgeMax = input.AgeMax
        };
        _dbContext.AgeRanges.Add(ageRange);
        await _dbContext.SaveChangesAsync();
    }
}
