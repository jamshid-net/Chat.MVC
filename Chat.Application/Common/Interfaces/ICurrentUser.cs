namespace Chat.Application.Common.Interfaces;
public interface ICurrentUser
{
    string? UserName { get; }
    Guid? UserId { get; }
}
