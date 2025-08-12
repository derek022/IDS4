using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpApi.Controllers;

[Route("api/identity")]
[Authorize("ApiScope")]
public class IdentityController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return new JsonResult(from claim in User.Claims select new { claim.Type, claim.Value });
    }
}