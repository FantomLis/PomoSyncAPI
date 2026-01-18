using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PomoSyncAPI.Backend.Database;
using PomoSyncAPI.Backend.Models;
using Serilog;

namespace PomoSyncAPI.Backend.Areas.Authorization;

/// <summary>
/// System authorization
/// </summary>
[ApiController]
[Route($"/api/{Executable.API_VERSION}/auth")]
public class AuthorizationController : Controller
{
    /// <summary>
    /// Creates new user token and protects it with password. Use this when client doesn't have token
    /// </summary>
    /// <param name="input">JSON-object with password</param>
    /// <param name="db">DI-injected DB</param>
    /// <response code="200">Returns newly created token</response>
    [HttpPost("token")]
    [Produces("application/json")]
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