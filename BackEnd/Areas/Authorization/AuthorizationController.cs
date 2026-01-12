using Microsoft.AspNetCore.Mvc;

namespace PomoSyncAPI.Backend.Areas.Authorization;

[Area("Authorization")]
[ApiController]
[Route("auth")]
public class AuthorizationController : Controller
{
    [HttpGet("token")]
    public IActionResult GenerateToken(string password)
    {
        return Ok();
    }
}