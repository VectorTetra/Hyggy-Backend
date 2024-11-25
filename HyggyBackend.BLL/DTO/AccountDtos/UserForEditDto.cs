
using System.ComponentModel.DataAnnotations;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class UserForEditDto
	{
		public string Id { get; set; }
		[Required(ErrorMessage = "Необхідно ввести ім'я.")]
		public string? Name { get; set; }
		[Required(ErrorMessage = "Необхідно ввести прізвище.")]
		public string? Surname { get; set; }
		[Required(ErrorMessage = "Необхідно ввести пошту.")]
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? AvatarPath { get; set; }
	}
}
