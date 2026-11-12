using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        [HttpPost("index")]
        public IActionResult Index([FromForm] string secretKey)
        {
            if (secretKey != "df2323eoT")
            {
                return Unauthorized();
            }

            return View(Data.Users);
        }

        [HttpGet("index")]
        public IActionResult IndexGet()
        {
            return Unauthorized();
        }
    }
}