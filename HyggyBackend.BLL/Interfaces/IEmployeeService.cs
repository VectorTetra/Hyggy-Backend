using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IEmployeeService<T> where T : EmployeeDTO
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetPaginatedEmployeesAsync(int? page);
		Task<IEnumerable<T>> GetEmployeesByProfessionAsync(string professionName);
		Task<IEnumerable<T>> GetEmployeesByDateOfBirthAsync(DateTime date);

		Task<T?> GetByIdAsync(string id);
		Task<T?> GetByNameAsync(string name);
		Task<T?> GetByEmail(string email);
		Task<T?> GetByPhoneAsync(string phone);

		Task CreateAsync(T item);
		void Update(T item);
		Task DeleteAsync(string id);
	}
}
