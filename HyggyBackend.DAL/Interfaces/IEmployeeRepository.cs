using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IEmployeeRepository<T> where T : Employee
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetPaginatedEmployeesAsync(int? page);
		Task<IEnumerable<T>> GetEmployeesByProfessionAsync(string professionName);
		Task<IEnumerable<T>> GetEmployeesByDateOfBirthAsync(DateTime date);
		Task<IEnumerable<T>> GetBySurnameAsync(string surname);

		Task<T?> GetByIdAsync(string id);
		Task<T?> GetByEmail(string email);
		Task<T?> GetByPhoneAsync(string phone);

		Task CreateAsync(T item);
		void Update(T item);
		Task DeleteAsync(string id);
	}
}
