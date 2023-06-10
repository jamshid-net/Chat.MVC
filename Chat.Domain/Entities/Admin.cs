using Chat.Domain.Common;

namespace Chat.Domain.Entities;
public class Admin:BaseEntity
{
    public string AdminName { get; set; }
    public string Password { get; set; }
    public string Role { get;set; }
}

