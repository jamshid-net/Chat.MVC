using Chat.Domain.Common;

namespace Chat.Domain.Entities;
public class User:BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public virtual ICollection<Message>? Messages { get; set; }

    public virtual ICollection<User>? Friends { get; set; }
}
