using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.Entities.Employes;
using Org.BouncyCastle.Tsp;

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
                if (!response.IsSuccessfullRegistration)
                    return BadRequest(response.Errors);

                return Ok("Надішлить співробітнику повідомлення про підтверження аккаунту.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            try
            {
                var result = await _service.EmailConfirmation(email, token);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("shopemployee/{shopId}")]
        public async Task<IActionResult> GetAllByShopId(long shopId)
        {
            try
            {
                var employees = await _service.GetEmployeesByWorkPlaceId(shopId);
                if (employees is null)
                    return NotFound();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("shopemployes")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _service.GetAllAsync();
                if (employees is null)
                    return NotFound();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("shopemployee-email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var employee = await _service.GetByEmail(email);
                if (employee is null)
                    return NotFound();

                return Ok(employee);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("shopemployee-surname")]
        public async Task<IActionResult> GetBySurname(string surname)
        {
            try
            {
                var employee = await _service.GetBySurnameAsync(surname);
                if (employee is null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("shopemployee-phone")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            try
            {

                var employee = await _service.GetByPhoneAsync(phone);
                if (employee is null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("editemployee")]
        public async Task<IActionResult> EditEmployee(ShopEmployeeDTO employeeDTO)
        {
            try
            {

                var employee = await _service.GetByIdAsync(employeeDTO.Id!);
                if (employee is null)
                    return NotFound();

                _service.Update(employeeDTO);

                return Ok("Співробітника оновлено.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("deleteemployee")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {

                var employee = await _service.GetByIdAsync(id);
                if (employee is null)
                    return NotFound();

                await _service.DeleteAsync(id);
                return Ok("Співробітника видалено.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
    }
}
