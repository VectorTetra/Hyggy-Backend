using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IEmployeeService<T> where T : EmployeeDTO
	{
		Task<IEnumerable<T>> GetAllAsync();
		
		Task<IEnumerable<T>> GetPaginatedEmployeesAsync(int? page);
		Task<IEnumerable<T>> GetEmployeesByProfessionAsync(string professionName);
		Task<IEnumerable<T>> GetEmployeesByWorkPlaceId(long id);
		Task<IEnumerable<T>> GetEmployeesByDateOfBirthAsync(DateTime date);

		Task<T?> GetByIdAsync(string id);
		Task<T?> GetByNameAsync(string name);
		Task<T?> GetByEmail(string email);
		Task<T?> GetByPhoneAsync(string phone);

		Task<ShopEmployeeDTO> Login(LoginDto login);
		Task<RegistrationResponseDto> CreateAsync(EmployeeForRegistrationDto registrationDto);
		Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto);
		Task<string> EmailConfirmation(string email, string code);
		void Update(T item);
		Task DeleteAsync(string id);
	}
}
