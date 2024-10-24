using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class UserForRegistrationDto
	{
		[Required(ErrorMessage = "Необхідно ввести ім'я.")]
		public string? Name { get; set; }
        [Required (ErrorMessage ="Необхідно ввести прізвище.")]
		public string? Surname { get; set; }

        [Required (ErrorMessage ="Необхідно ввести пошту.")]
        public string? Email { get; set; }

		[Required(ErrorMessage = "Необхідно ввести пароль.")]
		public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Підтвердить ваш пароль.")]
        public string? ConfirmPassword { get; set; }
        [JsonIgnore]
        public string? UserUri { get; set; } = "http://www.hyggy.somee.com/api/account/emailconfirmation";

    }
}
