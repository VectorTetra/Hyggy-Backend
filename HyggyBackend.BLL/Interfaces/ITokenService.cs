using HyggyBackend.DAL.Entities;

namespace HyggyBackend.BLL.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}
