using HyggyBackend.BLL.DTO.AccountDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
    [Authorize(Roles = "Store manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreManagerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Ви отримали доступ як Керуючий магазином");
        }
        
    }
}
