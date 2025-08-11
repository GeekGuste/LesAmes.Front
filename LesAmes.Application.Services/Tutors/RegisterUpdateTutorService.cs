using LesAmes.Domain.Authentication;
using LesAmes.Domain.Hobbies;
using LesAmes.Domain.Users;
using LesAmes.Dto.Tutors;
using LesAmes.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace LesAmes.Application.Services.Tutors;

public class RegisterUpdateTutorService : LesAmesApiService
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RegisterUpdateTutorService(IHttpContextAccessor? httpContextAccessor, AppDbContext dbContext, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task RegisterAsync(TutorDto input)
    {
        var tutor = new Tutor
        {
            LastName = input.LastName,
            FirstName = input.FirstName,
            Email = input.Email,
            PhoneNumber = input.PhoneNumber,
            BirthYear = input.BirthYear
        };

        var defaultPassword = getDefaultPassword(input);

        using var transaction = new TransactionScope();
        var createResult = await _userManager.CreateAsync(tutor, defaultPassword);
        if (!createResult.Succeeded)
            throw new Exception();

        if (!await _roleManager.RoleExistsAsync(Roles.Tutor))
            await _roleManager.CreateAsync(new ApplicationRole(Roles.Tutor));

        var addToRole = await _userManager.AddToRoleAsync(tutor, Roles.Tutor);
        if (!addToRole.Succeeded)
            throw new Exception();

        if (input.HobbiesIds?.Any() == true)
        {
            var hobbies = await _dbContext.Set<Hobby>()
                .Where(h => input.HobbiesIds.Contains(h.Id))
                .ToListAsync();

            foreach (var hobby in hobbies)
            {
                tutor.Hobbies.Add(hobby);
            }

            _dbContext.Update(tutor);
            await _dbContext.SaveChangesAsync();
        }
        transaction.Complete();
    }


    public Task UpdateCurrentUserAsync(TutorDto input)
    {
        var tutor = _dbContext.Tutors
            .Include(t => t.Hobbies)
            .FirstOrDefault(t => t.Id == CurrentUserId);
        if (tutor == null)
            throw new Exception("Tutor not found");
        tutor.FirstName = input.FirstName;
        tutor.LastName = input.LastName;
        tutor.PhoneNumber = input.PhoneNumber;
        tutor.BirthYear = input.BirthYear;
        if (input.HobbiesIds?.Any() == true)
        {
            var hobbies = _dbContext.Set<Hobby>()
                .Where(h => input.HobbiesIds.Contains(h.Id))
                .ToList();
            tutor.Hobbies.Clear();
            foreach (var hobby in hobbies)
            {
                tutor.Hobbies.Add(hobby);
            }
        }

        _dbContext.Update(tutor);
        return _dbContext.SaveChangesAsync();
    }

    private string getDefaultPassword(TutorDto input)
    {
        return input.FirstName.ToLower()[0] + input.LastName.ToLower() + input.BirthYear;
    }
}
