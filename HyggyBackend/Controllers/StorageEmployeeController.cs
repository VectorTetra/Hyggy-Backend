using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
    [ApiController]
    [Route("api/storageemployee")]
    public class StorageEmployeeController : Controller
    {
        private IEmployeeService<StorageEmployeeDTO> _service;
        private readonly IConfiguration _configuration;
        public StorageEmployeeController(IEmployeeService<StorageEmployeeDTO> service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] EmployeeForRegistrationDto registerDto)
        {

            try
            {
                if (registerDto is null)
                    return BadRequest();

                var response = await _service.Create(registerDto);
                if (!response.IsSuccessfullRegistration)
                    return BadRequest(response.Errors);

                return Ok("Відправте співробітнику повідомлення про підтверження аккаунту.");
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
            try
            {
                var pageRedirectUrlOk = _configuration["BaseUrls:EmployeeEmailConfirmedUrlOk"];
                var pageRedirectUrlError = _configuration["BaseUrls:EmployeeEmailConfirmedUrlError"];
                var result = await _service.EmailConfirmation(email, token);
                if (result)
                {
                    if (string.IsNullOrEmpty(pageRedirectUrlOk))
                    {
                        return StatusCode(500, "Redirect URL is not configured.");
                    }
                    return Redirect(pageRedirectUrlOk);
                }
                if (string.IsNullOrEmpty(pageRedirectUrlError))
                {
                    return StatusCode(500, "Redirect URL is not configured.");
                }
                return Redirect(pageRedirectUrlError);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
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
            var employee = await _service.GetBySurname(surname);
            if (employee is null)
                return NotFound();

            return Ok(employee);
        }
        [HttpGet("storageemployee-phone")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            var employee = await _service.GetByPhoneNumber(phone);
            if (employee is null)
                return NotFound();

            return Ok(employee);
        }
        [HttpGet("storageemployee-rolename")]
        public async Task<IActionResult> GetByRoleName(string rolename)
        {
            try
            {
                var employee = await _service.GetByRoleName(rolename);
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

        [HttpGet("storageemployee-query")]
        public async Task<IActionResult> GetByQuery([FromQuery] EmployeeQueryPL query)
        {
            try
            {
                var EmployeeMapper = new EmployeeMapperConfig();
                var mapperConfig = EmployeeMapper.EmployeeConfig;
                var mapper = mapperConfig.CreateMapper();
                var queryBLL = mapper.Map<EmployeeQueryBLL>(query);
                var employee = await _service.GetByQuery(queryBLL);
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
        public async Task<IActionResult> EditEmployee([FromBody] StorageEmployeeDTO employeeDTO)
        {
            try
            {
                var returnDTO = await _service.Update(employeeDTO);

                return Ok(returnDTO);
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
            var employee = await _service.GetById(id);
            if (employee is null)
                return NotFound();

            await _service.Delete(id);
            return Ok("Співробітника видалено.");
        }
    }
}
