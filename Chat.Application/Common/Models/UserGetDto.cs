using Chat.Domain.Entities;

namespace Chat.Application.Common.Models;
public class UserGetDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; } 

}



