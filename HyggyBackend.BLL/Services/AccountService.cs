using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace HyggyBackend.BLL.Services
{
	public class AccountService : IAccountService
	{
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly IEmailSender _emailSender;
		private readonly UserManager<User> _userManager;

		public AccountService(IMapper mapper,ITokenService tokenService, 
			IEmailSender emailSender, UserManager<User> userManager)
		{
			_mapper = mapper;
			_userManager = userManager;
			_tokenService = tokenService;
			_emailSender = emailSender;
		}
		public async Task<RegistrationResponseDto> RegisterAsync(UserForRegistrationDto registrationDto)
		{
			var user = _mapper.Map<Customer>(registrationDto);
			var result = await _userManager.CreateAsync(user, registrationDto.Password!);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);

				return new RegistrationResponseDto { IsSuccessfullRegistration = false, Errors = errors };
			}
			await _userManager.AddToRoleAsync(user, "User");

			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var param = new Dictionary<string, string?>
			{
				{ "token", token },
				{"email", user.Email }
			};

			var callback = QueryHelpers.AddQueryString(registrationDto.UserUri, param);
			var message = new Message([user.Email!], "Ласкаво просимо на Hyggy", callback);
			_emailSender.SendEmail(message);

			return new RegistrationResponseDto { IsSuccessfullRegistration = true, EmailToken = token };
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

			var callback = QueryHelpers.AddQueryString(passwordDto.ClientUri, param);
			var message = new Message([user.Email!], "Оновлення пароля на сайті Hyggy.ua", callback);
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
	}
}
