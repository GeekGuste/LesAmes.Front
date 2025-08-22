using LesAmes.Domain.Souls;
using LesAmes.Dto.Souls;
using LesAmes.Infrastructure.Database;

namespace LesAmes.Application.Services.Souls;

public class RegisterSoulsService
{
    private readonly AppDbContext _dbContext;

    public RegisterSoulsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegisterSoulAsync(SoulDto soulDto)
    {
        var soul = new Domain.Souls.Soul
        {
            Sexe = (Sexe)soulDto.Sexe,
            TutorId = soulDto.TutorId,
            FirstName = soulDto.FirstName,
            LastName = soulDto.LastName,
            Telephone = soulDto.Telephone,
            Quartier = soulDto.Quartier,
            Email = soulDto.Email,
            AgeRangeId = soulDto.AgeRangeId,
            DateCreation = soulDto.DateCreation,
        };
        _dbContext.Souls.Add(soul);
        await _dbContext.SaveChangesAsync();
    }
}
