using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RazorEngine;
using RazorEngine.Templating;
using System.Text;

namespace HyggyBackend.BLL.Services
{
	public class AccountService : IAccountService
	{
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly IEmailSender _emailSender;
		private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(IMapper mapper,ITokenService tokenService, 
			IEmailSender emailSender, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_mapper = mapper;
			_userManager = userManager;
			_tokenService = tokenService;
			_emailSender = emailSender;
            _roleManager = roleManager;
        }

		public async Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto)
		{
			var user = await _userManager.FindByNameAsync(authenticationDto.Email!);
			if(user is null)
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірне ім'я" };

			if(!await _userManager.CheckPasswordAsync(user, authenticationDto.Password!))
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірний пароль" };

			if (!await _userManager.IsEmailConfirmedAsync(user))
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Активуйте свій обліковий запис" };

			var token = await _tokenService.CreateToken(user);

			return new AuthResponseDto { IsAuthSuccessfull = true, Token = token };
		}
		public async Task<string> EmailConfirmation(string email, string token)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if(user is null)
				throw new ValidationException("Пошту не знайдено", email);

			var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
			if (!confirmResult.Succeeded)
				throw new ValidationException("Пошту не підтвержено", email);


			return "Обліковий запис підтвержено!";
		}
		public async Task<string> ForgotPassword(ForgotPasswordDto passwordDto)
		{
			var user = await _userManager.FindByEmailAsync(passwordDto.Email!);
			if (user is null)
				throw new ValidationException("Пошту не знайдено", passwordDto.Email!);

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			var param = new Dictionary<string, string?>
			{
				{ "token", token },
				{"email", user.Email }
			};
			passwordDto.ClientUri = $"http://localhost:3000/PagePasswordReset?reset={user.Id}";
			var callback = QueryHelpers.AddQueryString(passwordDto.ClientUri, param);
			var emailTemplate = EmailResetPasswordTemplate(user.Name, callback);

			var message = new Message([user.Email!], "Оновлення пароля на сайті Hyggy.ua", emailTemplate);
			_emailSender.SendEmail(message);

			return token;
		}
		public async Task<string> ResetPassword(ResetPasswordDto resetPasswordDto)
		{
			var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email!);
			if (user is null)
				throw new ValidationException("Пошту не знайдено", resetPasswordDto.Email!);

			var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token!, resetPasswordDto.Password!);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				throw new ValidationException(String.Join(',', errors), resetPasswordDto.Password!);
			}

			return "Ваш пароль оновлено";
		}
		//Оновлення аккаунта та видалення треба доробити
		public async Task<string> EditAccount(UserForEditDto userDto)
		{
			var user = await _userManager.FindByIdAsync(userDto.Id!);
			if(user is null)
				throw new ValidationException("Ваш аккаунт не знайдено", userDto.Id!);

			user = _mapper.Map<Customer>(userDto);

			var result = await _userManager.UpdateAsync(user);
			if(!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				throw new ValidationException(String.Join(',', errors), userDto.Id!);
			}

			return "Ваш аккаунт оновлено";
		}

		public async Task<string> Delete(string id)
		{
            var user = await _userManager.FindByIdAsync(id!);
            if (user is null)
                throw new ValidationException("Ваш аккаунт не знайдено", id!);

			var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ValidationException(String.Join(',', errors), id!);
            }

            return "Ваш аккаунт видалено";
        }
		
		private string EmailResetPasswordTemplate(string name, string callback)
		{
			var template = $@"
				<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
</head>
<body style=""background-color: #F8F8F8; margin: 60px; margin-top: 40px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; width: 800px;padding: 50px;"">
    <table style=""width: 100%; border-spacing: 0 20px;"">
        <tr>
            <td colspan=""2"" style=""padding: 0;"">
               <table style=""width: 100%; padding: 0; border-spacing: 0;"">
    <tr>
        <td style=""width: 50%;"">
            <img src=""https://s3-alpha-sig.figma.com/img/c71e/882e/8f5e30e3f0c3f65fa59a05f0a2b31601?Expires=1733097600&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=RC32EDo92P-pKnqdiA8XS0bgZVBmS2pcxK6xNkZD0eHbSZgC-clQm5JLRcIsPl7g2lVS59-NI4D2MAus68gHvWOCheMtkvf4eYQUGLUi0MI~03n7q3GhTiEusD6v-gtHr88iohFvE7OXgXbXqBo791X7MmLgawVkmrhZeGiHR16mf7MUmt~mzPTVtEtlxXvMlPsHGPm35Sx9Uly-U0~lHgBJfNqnEsCauWI~YDeRS36wghjpgzPSl7VLYt4bUVqRR9B6-EawazE-whMTcpvBndlHOWj-HyBmpqkM833KVJB24LeQ6LvKI0LVU25VsrrTJ9UtJN3oXOc6WH-JTTA8aw__"" style=""height: 70px; width: 120px;""/>
        </td>
        <td style=""width: 50%; text-align: right; vertical-align: bottom;"">
            <h3 style=""color: gray; font-weight: 100; margin: 0;"">Відділ по роботі з клієнтами</h3>
        </td>
    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td colspan=""2"" style=""background-color: white; padding: 0px;"">
                <table style=""width: 100%; border-spacing: 0;"">
                    <tr>
                        <td style=""text-align: center; border: 1px solid gray; height: 50px; padding: 0;"">
                            <a href=""http://localhost:3000/"" style=""color: #00AAAD; font-size: large; text-decoration: none; display: inline-block;"">Перейти на сайт Hyggy.ua</a>
                        </td>
                    </tr>
                    <tr >
                        <td style=""padding: 10px 20px;"">
                            <table style=""width: 100%; border-spacing: 0;"">
                                <tr>
                                    <td style=""text-align: right;"">
                                        <h2 style=""margin-top: 50px; margin-bottom: 0;"">Оновлення пароля на сайті Hyggy.ua</h2>
                                        <h4 style=""margin-top: 0; font-weight: 100;"">{DateTime.Now}</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style=""font-size: 18px; font-weight: 400;"">
                                            Вітаємо {name}<br/><br/>
                                            Забули пароль?<br/><br/>Без проблем. Просто натисніть на посилання, щоб скинути пароль.
											<br/><br/>Зверніть увагу, що посилання активне лише 4 години.<br/>
                                            <a href=""{callback}"" style=""display: inline-block; padding: 10px 20px; background-color: #00AAAD; color: white; text-decoration: none; border-radius: 5px; font-weight: bolder; margin-top: 20px;"">Скинути пароль</a>
                                            
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan=""2"" style=""padding: 10px 20px; "">
                            <h3 style=""margin: 0;"">ВІДДІЛ ПО РОБОТІ З КЛІЄНТАМИ</h3>
                            <p style=""font-size: 18px; font-weight: 400; margin: 10px 0;"">
                                У Вас виникли запитання чи потрібна допомога? 
                                <a href=""#"" style=""text-decoration: none; color: #00AAAD;"">Зверніться до Відділу по роботі з клієнтами</a>
                            </p>
                            <a href=""#"" style=""text-decoration: none; color: #00AAAD;"">
                                Адреса та години роботи магазину
                            </a>
                            <p style=""margin-top: 20px; font-size: 18px;"">
                                З повагою,<br/>
                                <span style=""color: #00AAAD; font-weight: bolder;"">Hyggy</span>
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
</body>
</html>


				";

			return template;
		}

	}
}
