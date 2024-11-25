using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/storageemployee")]
	public class StorageEmployeeController : Controller
	{
		private IEmployeeService<StorageEmployeeDTO> _service;

		public StorageEmployeeController(IEmployeeService<StorageEmployeeDTO> service)
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
				if (!response.IsSuccessfullRegistration)
					return BadRequest(response.Errors);

				return Ok("Надішлить співробітнику повідомлення про підтверження аккаунту.");
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
			catch (Exception ex)
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

		//[HttpGet("storageemployee/{shopId}")]
		//public async Task<IActionResult> GetAllByShopId(long shopId)
		//{
		//	var employees = await _service.GetEmployeesByWorkPlaceId(shopId);
		//	if (employees is null)
		//		return NotFound();

		//	return Ok(employees);
		//}
		[HttpGet("storageemployees")]
		public async Task<IActionResult> GetAll()
		{
			var employees = await _service.GetAllAsync();
			if (employees is null)
				return NotFound();

			return Ok(employees);
		}
		[HttpGet("storageemployee-email")]
		public async Task<IActionResult> GetByEmail(string email)
		{
			var employee = await _service.GetByEmail(email);
			if (employee is null)
				return NotFound();

			return Ok(employee);
		}
		[HttpGet("storageemployee-surname")]
		public async Task<IActionResult> GetBySurname(string surname)
		{
			var employee = await _service.GetBySurnameAsync(surname);
			if (employee is null)
				return NotFound();

			return Ok(employee);
		}
		[HttpGet("storageemployee-phone")]
		public async Task<IActionResult> GetByPhone(string phone)
		{
			var employee = await _service.GetByPhoneAsync(phone);
			if (employee is null)
				return NotFound();

			return Ok(employee);
		}
		[HttpPut("editemployee")]
		public async Task<IActionResult> EditEmployee([FromBody] StorageEmployeeDTO employeeDTO)
		{
			var employee = await _service.GetByIdAsync(employeeDTO.Id!);
			if (employee is null)
				return NotFound();

			_service.Update(employeeDTO);

			return Ok("Співробітника оновлено.");
		}
		[HttpDelete("deleteemployee")]
		public async Task<IActionResult> DeleteEmployee(string id)
		{
			var employee = await _service.GetByIdAsync(id);
			if (employee is null)
				return NotFound();

			await _service.DeleteAsync(id);
			return Ok("Співробітника видалено.");
		}
	}
}
