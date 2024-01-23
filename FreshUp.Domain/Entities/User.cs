using FreshUp.Domain.Commons;
using FreshUp.Domain.Enums;

namespace FreshUp.Domain.Entities;

public sealed class User : Auditable
{
    public string UserName { get; set; }    
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}
