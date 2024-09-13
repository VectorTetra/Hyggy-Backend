using System.ComponentModel.DataAnnotations;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class UserForAuthenticationDto
	{
		[Required(ErrorMessage = "Необхідно ввести вашу пошту.")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Необхідно ввести пароль.")]
		public string? Password { get; set; }
	}
}
