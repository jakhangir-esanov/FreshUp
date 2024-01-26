using System.Text;
using System.Security.Claims;
using FreshUp.Domain.Exceptions;
using FreshUp.Application.Helpers;
using FreshUp.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FreshUp.WebApi.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly IRepository<User> userRepository;
    public AuthService(IConfiguration configuration, IRepository<User> userRepository)
    {
        this.configuration = configuration;
        this.userRepository = userRepository;
    }

    public async Task<string> GenerateTokenAsync(string email, string password)
    {
        var user = await userRepository.SelectAsync(x => x.Email.Equals(email))
            ?? throw new NotFoundException("User not found!");

        bool varifiedPassword = PasswordHasher.Verify(password, user.Password);
        if (!varifiedPassword)
            throw new CustomException(400, "Password or Email is incorrect!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
}
