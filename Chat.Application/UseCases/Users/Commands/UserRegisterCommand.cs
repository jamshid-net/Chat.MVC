using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Chat.Application.UseCases.Users.Commands;
public class UserRegisterCommand : IRequest<ClaimsIdentity>
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string PasswordConfirm { get; init; }

}
public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ClaimsIdentity>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashStringService _hashStringService;
    private readonly IConfiguration _configuration;
    private readonly IAppCache _cache;

    public UserRegisterCommandHandler(IApplicationDbContext context, IHashStringService hashStringService, IAppCache cache, IConfiguration configuration)
    {
        _context = context;
        _hashStringService = hashStringService;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task<ClaimsIdentity> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        _cache.Remove(_configuration.GetValue<string>("LazyCache:UserKey"));
        string hashedPassword = await _hashStringService.GetHashStringAsync(request.Password);

        User user = new User()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = hashedPassword,
            UserName = request.UserName

        };
        _context.Users.Add(user);
        if (await _context.SaveChangesAsync(cancellationToken) > 0)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, request.UserName) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }
        return null;
    }
}