using LesAmes.Domain.Souls;
using LesAmes.Dto.Souls;
using LesAmes.Infrastructure.Database;

namespace LesAmes.Application.Services.Souls.ImpactFamilies;

public class CreateImpactFamilyService
{
    private readonly AppDbContext _dbContext;

    public CreateImpactFamilyService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateImpactFamilyAsync(CreateImpactFamilyInput input)
    {
        var impactFamily = new ImpactFamily
        {
            Name = input.Name,
            Address = input.Address,
            Quartiers = input.Quartiers,
            PilotName = input.PilotName,
            PilotContact = input.PilotContact
        };

        _dbContext.ImpactFamilies.Add(impactFamily);
        await _dbContext.SaveChangesAsync();
    }
}
