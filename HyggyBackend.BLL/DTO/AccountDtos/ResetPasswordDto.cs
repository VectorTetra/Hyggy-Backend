using System.ComponentModel.DataAnnotations;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class ResetPasswordDto
	{
		[Required(ErrorMessage = "Необхідно ввести пароль.")]
		public string? Password { get; set; }

		[Compare("Password", ErrorMessage = "Підтвердить ваш пароль.")]
		public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }
        public string? Token { get; set; }
	}
}
