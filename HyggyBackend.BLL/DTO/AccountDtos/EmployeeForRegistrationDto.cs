﻿
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class EmployeeForRegistrationDto
	{
		[Required(ErrorMessage = "Необхідно ввести ім'я.")]
		public string? Name { get; set; }
		[Required(ErrorMessage = "Необхідно ввести прізвище.")]
		public string? Surname { get; set; }
		[Required(ErrorMessage = "Необхідно ввести пошту.")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Необхідно ввести пароль.")]
		public string? Password { get; set; }
		[Compare("Password", ErrorMessage = "Підтвердить ваш пароль.")]
		public string? ConfirmPassword { get; set; }
		public string? PhoneNumber { get; set; }
		public string? RoleName { get; set; } = "Admin";
		public long? ShopId { get; set; } 
		public long? StorageId { get; set; }
		//[Required]
		[JsonIgnore]
		public string? UserUri { get; set; }
	}
}
