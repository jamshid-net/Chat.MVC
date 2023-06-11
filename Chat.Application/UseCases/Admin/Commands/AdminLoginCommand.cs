using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.UseCases.Admin.Commands;

public class AdminLoginCommand : IRequest<ClaimsIdentity>
{
    public string UserName { get; init; }
    public string Password { get; init; }

}
public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, ClaimsIdentity>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashStringService _hashStringService;

    public AdminLoginCommandHandler(IApplicationDbContext context, IHashStringService hashStringService)
    {
        _context = context;
        _hashStringService = hashStringService;
    }

    public async Task<ClaimsIdentity> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword = await _hashStringService.GetHashStringAsync(request.Password);
        var entity = _context.Admins.SingleOrDefault(x => x.AdminName == request.UserName && x.Password == hashedPassword);
        if (entity is null)
            throw new NotFoundException(nameof(Admin), request.UserName);

        var claims = new List<Claim>() 
        { 
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(ClaimTypes.Role,entity.Role) 
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}
