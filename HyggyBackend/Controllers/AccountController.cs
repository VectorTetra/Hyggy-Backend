
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HyggyBackend.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] UserForRegistrationDto registrationDto)
        //{
        //	try
        //	{
        //		if (registrationDto is null)
        //			return BadRequest();

        //		var response = await _service.RegisterAsync(registrationDto);
        //		if (!response.IsSuccessfullRegistration)
        //			return BadRequest(response.Errors);

        //		return Ok(new { message = "Будь ласка підтвердіть ваш обліковий запис" });
        //	}
        //          catch (Exception ex)
        //          {
        //              if (ex.InnerException != null)
        //              {
        //                  return StatusCode(500, ex.InnerException.Message);
        //              }
        //              return StatusCode(500, ex.Message);
        //          }
        //      }
        //[HttpPost("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto authenticationDto)
        //{
        //	try
        //	{
        //		var response = await _service.AuthenticateAsync(authenticationDto);
        //		if (!response.IsAuthSuccessfull)
        //			return StatusCode(500, response.Error);

        //		return Ok(response);
        //	}
        //          catch (Exception ex)
        //          {
        //              if (ex.InnerException != null)
        //              {
        //                  return StatusCode(500, ex.InnerException.Message);
        //              }
        //              return StatusCode(500, ex.Message);
        //          }
        //      }
        //[HttpGet("emailconfirmation")]
        //public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        //{
        //	try
        //	{
        //		var result = await _service.EmailConfirmation(email, token);

        //		return Ok(result);
        //	}
        //	catch(ValidationException ex)
        //	{
        //		return StatusCode(500, ex.Message);
        //	}
        //          catch (Exception ex)
        //          {
        //              if (ex.InnerException != null)
        //              {
        //                  return StatusCode(500, ex.InnerException.Message);
        //              }
        //              return StatusCode(500, ex.Message);
        //          }
        //      }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.ForgotPassword(forgotPassword);
                return Ok(result);
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
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.ResetPassword(resetPassword);
                return Ok(result);
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
        //[HttpPut("editaccount")]
        //public async Task<IActionResult> EditAccount([FromBody] UserForEditDto userDto)
        //{
        //	try
        //	{
        //		var result = await _service.EditAccount(userDto);

        //		return Ok(result);
        //	}
        //	catch(ValidationException ex)
        //	{
        //		return StatusCode(500, ex.Message);
        //	}
        //          catch (Exception ex)
        //          {
        //              if (ex.InnerException != null)
        //              {
        //                  return StatusCode(500, ex.InnerException.Message);
        //              }
        //              return StatusCode(500, ex.Message);
        //          }
        //      }
        //Закоментував, що було раніше

        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;

        //public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //    _configuration = configuration;
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] Register model)
        //{
        //    var user = new IdentityUser
        //    {
        //        UserName = model.Email,
        //        Email = model.Email
        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (result.Succeeded)
        //    {
        //        await _userManager.AddToRoleAsync(user, "User");

        //        return Ok(new { message = "Користувача успішно зареєстровано!" });
        //    }

        //    return BadRequest(result.Errors);
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] Login model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };

        //        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        //        var token = new JwtSecurityToken(
        //                issuer: _configuration["JWT:ValidIssuer"],
        //                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
        //                claims: authClaims,
        //                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256));

        //        return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        //    }
        //    return Unauthorized();
        //}

        //[HttpPost("add-role")]
        //public async Task<IActionResult> AddRole([FromBody] string role)
        //{
        //	if (!await _roleManager.RoleExistsAsync(role))
        //	{
        //		var result = await _roleManager.CreateAsync(new IdentityRole(role));
        //		if (result.Succeeded)
        //		{
        //			return Ok(new { message = "Роль успішно додано!" });
        //		}
        //		else { return BadRequest(result.Errors); }

        //	}
        //	else
        //	{
        //		return BadRequest(new { message = "Така роль вже існує!" });
        //	}
        //}

        //[HttpPost("assign-role")]
        //public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        //{
        //	var user = await _userManager.FindByEmailAsync(model.Email);


        //	if (user != null)
        //	{
        //		if (await _roleManager.RoleExistsAsync(model.Role))
        //		{
        //			await _userManager.AddToRoleAsync(user, model.Role);
        //			return Ok(new { message = "Роль успішно назначено!" });
        //		}
        //		else
        //		{
        //			return BadRequest(new { message = "Такої ролі не існує!" });
        //		}
        //	}
        //	else
        //	{
        //		return BadRequest(new { message = "Користувача не знайдено!" });
        //	}
        //}
    }
}
