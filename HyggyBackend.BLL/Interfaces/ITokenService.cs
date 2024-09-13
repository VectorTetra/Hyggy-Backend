using HyggyBackend.DAL.Entities;

namespace HyggyBackend.BLL.Interfaces
{
	public interface ITokenService
	{
		Task<string> CreateToken(User user);
	}
}
