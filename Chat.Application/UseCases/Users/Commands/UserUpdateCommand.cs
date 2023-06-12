using Chat.Application.Common.Exceptions;
using Chat.Application.Common.Interfaces;
using Chat.Domain.Entities;
using LazyCache;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Chat.Application.UseCases.Users.Commands;
public class UserUpdateCommand:IRequest<bool>
{
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }  
    public string Password { get; init; }
}
public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashStringService _hashString;
    private readonly IConfiguration _configuration;
    private readonly IAppCache _cache;

    public UserUpdateCommandHandler(IApplicationDbContext context, IHashStringService hashString, IAppCache cache, IConfiguration configuration)
    {
        _context = context;
        _hashString = hashString;
        _cache = cache;
        _configuration = configuration;
    }
    public async Task<bool> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        _cache.Remove(_configuration.GetValue<string>("LazyCache:UserKey"));
        var entity = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(User), request.UserId);

        var properties = typeof(UserUpdateCommand).GetProperties();

        foreach (var property in properties)
        {
            var requestValue = property.GetValue(request);
            if (requestValue is not null && property.Name is not "UserId")
            {
                var entityProperty = entity.GetType().GetProperty(property.Name);
                entityProperty.SetValue(entity, requestValue);

            }

            if (property.Name is "Password" && requestValue is not null)
            {
                entity.Password = await _hashString.GetHashStringAsync(request.Password);
            }

        }
      
        int savechanges = await _context.SaveChangesAsync(cancellationToken);

        if(savechanges > 0)
            return true;
        return false;
    }
        
}
