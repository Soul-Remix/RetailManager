using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RetailManager.Data;
using RetailManager.DTO.Auth;
using RetailManager.Interfaces;
using RetailManager.Models;
using RetailManager.ViewModels.Auth;

namespace RetailManager.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AppDbContext _context;

    public AuthService(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task RegisterUserAsync(RegisterDto model)
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

        var account = new Profile
        {
            Id = user.Id,
            FirstName = "",
            LastName = "",
            Email = user.Email,
        };
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<LoginResponse> LoginUserAsync(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var signin = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!signin.Succeeded)
        {
            throw new Exception("Failed to login");
        }

        var token = CreateJwt(user, roles);
        return new LoginResponse
        {
            Token = token,
            ExpiryDate = DateTime.Now.AddDays(7),
            ExpiresIn = 604800,
        };
    }

    private static string CreateJwt(IdentityUser user, IList<string>? roles)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this is my secret"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, user.Id)
        };
        if (roles != null)
        {
            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }
        }

        var token = new JwtSecurityToken(
            issuer: "ad",
            audience: "ad",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}