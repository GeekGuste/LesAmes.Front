using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LesAmes.Application.Services;

public class LesAmesApiService : ILesAmesApiService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LesAmesApiService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? CurrentUserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
