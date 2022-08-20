using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RetailManager.Data;


namespace RetailManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;

    public RolesController(UserManager<IdentityUser> userManager,
     RoleManager<IdentityRole> roleManager,
     AppDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    // GET: api/roles 
    [HttpGet]
    public async Task<List<IdentityRole>> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles;
    }

    [HttpPost]
    public async Task<ActionResult> CreateRole(string roleName)
    {
        var role = new IdentityRole(roleName);
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
        {
            return BadRequest();
        }
        var roleExist = await _roleManager.CreateAsync(role);
        return Ok();
    }

    [HttpPut]
    [Route("user/{id}")]
    public async Task<ActionResult> AddRoleToUser(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (roleExists)
        {
            return NotFound();
        }

        await _userManager.AddToRoleAsync(user, role);
        return NoContent();
    }

    [HttpDelete]
    [Route("user/{id}")]
    public async Task<ActionResult> RemoveUserFromRole(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (roleExists)
        {
            return NotFound();
        }

        await _userManager.RemoveFromRoleAsync(user, role);
        return NoContent();
    }
}