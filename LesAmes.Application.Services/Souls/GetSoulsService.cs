using LesAmes.Dto.Souls;
using LesAmes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LesAmes.Application.Services.Souls;

public class GetSoulsService
{
    public AppDbContext _dbContext { get; }

    public GetSoulsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetSoulsOutput>> GetAllSoulsAsync()
    {
        var souls = await _dbContext.Souls.OrderBy(s => s.DateCreation).ToListAsync();

        return souls.Select(s => new GetSoulsOutput
        {
            Souls = new List<SoulElementDto>
            {
                new SoulElementDto
                {
                    Id = s.Id.ToString(),
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Sexe = (SexeDto)s.Sexe,
                    Telephone = s.Telephone,
                    Quartier = s.Quartier,
                    Email = s.Email,
                    AgeRangeId = s.AgeRangeId,
                    DateCreation = s.DateCreation
                }
            }
        }).ToList();
    }
}
