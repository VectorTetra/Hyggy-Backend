﻿using HyggyBackend.BLL.DTO.AccountDtos;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IAccountService
	{
		Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto);
		Task<bool> EmailConfirmation(string email, string code);
		Task<string> ForgotPassword(ForgotPasswordDto passwordDto);
		Task<string> ResetPassword(ResetPasswordDto resetPasswordDto);
		Task<string> EditAccount(UserForEditDto userDto);
		Task<string> Delete(string id);
	}
}
