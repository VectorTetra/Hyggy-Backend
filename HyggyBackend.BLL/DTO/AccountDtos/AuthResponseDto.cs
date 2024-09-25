

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class AuthResponseDto 
	{
		public bool IsAuthSuccessfull{ get; set; }
		public string? Error { get; set; }
		public string? Token { get; set; }
	}
}
