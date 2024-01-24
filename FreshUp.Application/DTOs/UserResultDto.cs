namespace FreshUp.Application.DTOs;

public class UserResultDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}
