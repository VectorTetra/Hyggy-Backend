using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Services.EmailService;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/employee")]
	public class EmployeeController : Controller
	{
		private IEmployeeService<ShopEmployeeDTO> _service;
		public EmployeeController(IEmployeeService<ShopEmployeeDTO> service)
		{
			_service = service;
		}
		
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] EmployeeForRegistrationDto registerDto)
		{
			
			try
			{
				if (registerDto is null)
					return BadRequest();

				var response = await _service.CreateAsync(registerDto);
				if(!response.IsSuccessfullRegistration)
					return BadRequest(response.Errors);

				//return Ok(new {message = $"Будь ласка підтвердить вашу пошту {response.EmailToken}"});
				return Ok("Співробітника додано");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
			
		}
		
		[HttpPost("authenticate")]
		public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto authentication)
		{
			try
			{
				var response = await _service.AuthenticateAsync(authentication);
				if (!response.IsAuthSuccessfull)
					return StatusCode(500, response.Error);

				return Ok(response);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);

			}
		}
		[HttpGet("emailconfirmation")]
		public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
		{
			var result = await _service.EmailConfirmation(email, token);

			return Ok(result);
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
