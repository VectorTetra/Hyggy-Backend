using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
    [Route("api/Proffession")]
    [ApiController]
    public class ProffessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
