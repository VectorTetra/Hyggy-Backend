using System.Reflection.Metadata.Ecma335;

namespace HyggyBackend.BLL.DTO.AccountDtos
{
	public class RegistrationResponseDto 
	{
        public bool IsSuccessfullRegistration { get; set; }
		public IEnumerable<string>? Errors { get; set; }
        public string? EmailToken { get; set; }
    }
}
