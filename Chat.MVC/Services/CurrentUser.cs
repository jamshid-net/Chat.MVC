using Chat.Application.Common.Interfaces;
using System.Security.Claims;

namespace Chat.MVC.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _applicationDbContext;

    public CurrentUser(IHttpContextAccessor httpcontextAccessor, IApplicationDbContext applicationDbContext)
          => (_httpContextAccessor, _applicationDbContext) = (httpcontextAccessor, applicationDbContext);
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    public Guid? UserId => _applicationDbContext?.Users?.SingleOrDefault(x => x.UserName == this.UserName)?.Id;
}
