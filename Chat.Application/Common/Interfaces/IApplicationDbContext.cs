using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }

    public DbSet<Message> Messages { get; } 

    public DbSet<Admin> Admins { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
