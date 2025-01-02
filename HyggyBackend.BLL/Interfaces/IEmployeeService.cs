using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IEmployeeService<T> where T : EmployeeDTO
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetPaged(int pageNumber, int PageSize);
		Task<IEnumerable<T>> GetEmployeesByWorkPlaceId(long id);
		Task<IEnumerable<T>> GetEmployeesByDateOfBirth(DateTime date);
		Task<IEnumerable<T>?> GetBySurname(string surname);
        Task<IEnumerable<T>> GetByName(string name);
        Task<IEnumerable<T>> GetByStringIds(string stringIds);
        Task<T?> GetById(string id);
		Task<T?> GetByEmail(string email);
		Task<T?> GetByPhoneNumber(string phone);
        Task<IEnumerable<T>> GetByRoleName(string roleName);
        Task<string?> GetRoleName(string employeeId);
        Task<IEnumerable<T>> GetByQuery(EmployeeQueryBLL query);
        Task<RegistrationResponseDto> Create(EmployeeForRegistrationDto registrationDto);
		Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto);
		Task<bool> EmailConfirmation(string email, string code);
		Task<T> Update(T item);
		Task Delete(string id);
	}
}
