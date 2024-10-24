using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class ForgotPasswordDto
	{
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [JsonIgnore]
        public string? ClientUri { get; set; } = "http://www.hyggy.somee.com/api/account/resetpassword";

	}
}
