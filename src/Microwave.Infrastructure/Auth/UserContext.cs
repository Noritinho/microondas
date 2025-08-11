using Microsoft.AspNetCore.Http;
using Microwave.Domain.Interfaces;

namespace Microwave.Infrastructure.Auth;
public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public int Id => int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
}

