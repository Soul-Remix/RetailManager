using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailManager.Data;
using RetailManager.DTO;
using RetailManager.Models;

namespace RetailManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/users/profile
    [HttpGet("profile")]
    public async Task<ActionResult<Profile>> GetUserDetails()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _context.Profiles.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/users/id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(string id, ProfileDto model)
    {
        var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!User.IsInRole("Admin") && loggedInUserId != id)
        {
            return BadRequest();
        }

        var userToUpdate = await _context.Profiles.FindAsync(id);
        if (userToUpdate == null)
        {
            return NotFound();
        }

        userToUpdate.FirstName = model.FirstName;
        userToUpdate.LastName = model.LastName;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Admin Routes

    // GET: api/users
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Profile>>> GetUsers()
    {
        var users = await _context.Profiles.AsNoTracking().ToListAsync();
        return users;
    }
}