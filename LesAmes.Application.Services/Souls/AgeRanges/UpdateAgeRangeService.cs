using LesAmes.Dto.Souls.AgeRanges;
using LesAmes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Application.Services.Souls.AgeRanges;

public class UpdateAgeRangeService
{
    private readonly AppDbContext _dbContext;

    public UpdateAgeRangeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAgeRangeAsync(string id, AgeRangeInput input)
    {
        var ageRange = await _dbContext.AgeRanges.FindAsync(id);
        if (ageRange == null)
        {
            throw new KeyNotFoundException($"Age range with ID {id} not found.");
        }

        var existantAgeRange = await _dbContext.AgeRanges.Where(ar => ar.AgeMin == input.AgeMin && ar.AgeMax == input.AgeMax && ar.Id != id).FirstOrDefaultAsync();
        if (existantAgeRange != null)
        {
            throw new InvalidOperationException("Cette tranche d'age existe déjà.");
        }

        ageRange.AgeMin = input.AgeMin;
        ageRange.AgeMax = input.AgeMax;
        _dbContext.AgeRanges.Update(ageRange);
        await _dbContext.SaveChangesAsync();
    }
}
