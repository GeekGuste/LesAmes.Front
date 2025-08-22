using LesAmes.Domain.Souls;
using LesAmes.Dto.Souls;
using LesAmes.Infrastructure.Database;

namespace LesAmes.Application.Services.Souls;

public class UpdateSoulsService
{
    private readonly AppDbContext _dbContext;

    public UpdateSoulsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateSoulAsync(string soulId, SoulDto soulDto)
    {
        var soul = await _dbContext.Souls.FindAsync(soulId);
        if (soul == null)
        {
            throw new KeyNotFoundException("Soul not found.");
        }
        soul.Sexe = (Sexe)soulDto.Sexe;
        soul.TutorId = soulDto.TutorId;
        soul.FirstName = soulDto.FirstName;
        soul.LastName = soulDto.LastName;
        soul.Telephone = soulDto.Telephone;
        soul.Quartier = soulDto.Quartier;
        soul.Email = soulDto.Email;
        soul.AgeRangeId = soulDto.AgeRangeId;
        soul.DateCreation = soulDto.DateCreation;
        _dbContext.Souls.Update(soul);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetSoulTutor(string soulId, string tutorId)
    {
        var soul = await _dbContext.Souls.FindAsync(soulId);
        if (soul == null)
        {
            throw new KeyNotFoundException("Soul not found.");
        }
        soul.TutorId = tutorId;
        _dbContext.Souls.Update(soul);
        await _dbContext.SaveChangesAsync();
    }
}
