using LesAmes.Dto.Souls;
using LesAmes.Infrastructure.Database;

namespace LesAmes.Application.Services.Souls.ImpactFamilies;

public class UpdateImpactFamilyService
{
    private readonly AppDbContext _dbContext;

    public UpdateImpactFamilyService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(string id, UpdateImpactFamilyInput input)
    {
        var impactFamily = await _dbContext.ImpactFamilies.FindAsync(id);
        if (impactFamily == null)
        {
            throw new KeyNotFoundException("Impact family not found.");
        }
        impactFamily.Name = input.Name;
        impactFamily.Address = input.Address;
        impactFamily.Quartiers = input.Quartiers;
        impactFamily.PilotName = input.PilotName;
        impactFamily.PilotContact = input.PilotContact;

        _dbContext.ImpactFamilies.Update(impactFamily);
        await _dbContext.SaveChangesAsync();
    }
}
