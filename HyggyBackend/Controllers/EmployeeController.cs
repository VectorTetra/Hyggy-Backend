using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeeController : Controller
	{
		private IEmployeeService<ShopEmployeeDTO> _service;
        public EmployeeController(IEmployeeService<ShopEmployeeDTO> service)
        {
            _service = service;
        }
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var emp = await _service.CreateAsync(registerDto);
			return Ok(emp);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _service.Login(loginDto);
			return Ok(result);
		}
		
		
		[HttpGet("shop-employees")]
		public async Task<IActionResult> GetAll(long shopId)
		{
			return Ok(await _service.GetEmployeesByWorkPlaceId(shopId));
		}


	}
}
