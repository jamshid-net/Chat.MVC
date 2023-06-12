using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Chat.Application.UseCases.Users.Commands;
public class UserCreateCommand:IRequest<Guid>
{
    public string UserName { get; init; } 
    public string Email { get; init; }  
    public string Password { get; init; }
}
public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IAppCache _cache;
    private readonly IConfiguration _configuration;
    private readonly IHashStringService _hashStringService;

    public UserCreateCommandHandler(IApplicationDbContext context, IAppCache cache, IConfiguration configuration, IHashStringService hashStringService)
    {
        _context = context;
        _cache = cache;
        _configuration = configuration;
        _hashStringService = hashStringService;
    }

    public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword =await _hashStringService.GetHashStringAsync(request.Password);
        _cache.Remove(_configuration.GetValue<string>("LazyCache:UserKey"));
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = hashedPassword,
            UserName = request.UserName,
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    
    }
}
