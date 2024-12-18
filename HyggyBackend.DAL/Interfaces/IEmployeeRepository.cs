using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IEmployeeRepository<T> where T : Employee
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetPaged(int pageNumber, int PageSize);
		Task<IEnumerable<T>> GetEmployeesByDateOfBirth(DateTime date);
		Task<IEnumerable<T>> GetBySurname(string surname);
		Task<IEnumerable<T>> GetByName(string name);
		Task<IEnumerable<T>> GetByStringIds(string stringIds);
		Task<T?> GetById(string id);
		Task<T?> GetByEmail(string email);
		Task<T?> GetByPhoneNumber(string phone);
		Task<IEnumerable<T>> GetByRoleName(string roleName);
		Task<string?> GetRoleName(string employeeId);
		Task<IEnumerable<T>> GetByQuery(EmployeeQueryDAL queryDAL);
        IAsyncEnumerable<T> GetByIdsAsync(IEnumerable<string> ids);
        Task Create(T item);
		void Update(T item);
		Task Delete(string id);
	}
}
