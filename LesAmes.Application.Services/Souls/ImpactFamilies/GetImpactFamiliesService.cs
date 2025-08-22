using LesAmes.Dto.Souls;
using LesAmes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Application.Services.Souls.ImpactFamilies;
public class GetImpactFamiliesService
{
    private readonly AppDbContext _dbContext;

    public GetImpactFamiliesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetImpactFamiliesOutput> GetAllImpactFamiliesAsync()
    {
        return new GetImpactFamiliesOutput
        {
            ImpactFamilies = await _dbContext.ImpactFamilies
                .AsNoTracking()
                .OrderBy(imp => imp.IsActive)
                .ThenBy(imp => imp.Name)
                .Select(impactFamily => new ImpactFamilyDto
                {
                    Id = impactFamily.Id,
                    Name = impactFamily.Name,
                    Address = impactFamily.Address,
                    Quartiers = impactFamily.Quartiers,
                    PilotName = impactFamily.PilotName,
                    PilotContact = impactFamily.PilotContact,
                    IsActive = impactFamily.IsActive
                })
                .ToListAsync()
        };
    }
}
