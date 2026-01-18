using Microsoft.AspNetCore.Mvc;
using PomoSyncAPI.Backend.Database;
using PomoSyncAPI.Backend.Models;
using Serilog;

namespace PomoSyncAPI.Backend.Areas.Authorization;

[ApiController]
[Route($"/api/{Executable.API_VERSION}/auth")]
public class AuthorizationController : Controller
{
    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenInput input, MainDatabaseContext db)
    {
        User u = new User()
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password)
        };
        await db.UserTable.AddAsync(u);
        await db.SaveChangesAsync();
        Log.Information($"Created new user {u.Token}.");
        return Ok(new GenerateTokenDTO() {Token = u.Token.ToString()});
    }
}