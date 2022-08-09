using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RetailManager.DTO.Auth;
using RetailManager.Interfaces;
using RetailManager.ViewModels.Auth;

namespace RetailManager.Services;

public class AuthService : IAuthService
{
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task RegisterUserAsync(RegisterDTO model)
    {
        var user = new IdentityUser
        {
            Email = model.Email,
            UserName = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to create user account");
        }
    }

    public async Task<LoginResponse> LoginUserAsync(LoginDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var signin = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!signin.Succeeded)
        {
            throw new Exception("Failed to login");
        }

        var token = CreateJwt(user);
        return new LoginResponse
        {
            Token = token,
            ExpiryDate = DateTime.Now.AddDays(7),
            ExpiresIn = 1660673038
        };
    }

    private static string CreateJwt(IdentityUser user)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this is my secret"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,user.Id)
        };

        var token = new JwtSecurityToken(
            issuer: "ad", 
            audience: "ad", 
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials:credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}