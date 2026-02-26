using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    [HttpGet("admin-only")]
    public IActionResult AdminEndpoint()
    {
        return Ok("Admin access granted");
    }
    
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return Ok("Admin dashboard data");
    }
}