using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories.Employes
{
    public class ShopEmployeeRepository: IEmployeeRepository<ShopEmployee>
    {
		private readonly HyggyContext _context;
		public ShopEmployeeRepository(HyggyContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<ShopEmployee>> GetAllAsync()
		{
			return await _context.ShopEmployees
				.Include(se => se.Proffession)
				.Include(se => se.Shop).ToListAsync();
		}
		public async Task<IEnumerable<ShopEmployee>> GetPaginatedEmployeesAsync(int? page)
		{
			const int pageSize = 10;
			var employees = await GetAllAsync();
			var paginatedEmployees = employees.Skip((page ?? 0) *  pageSize)
				.Take(pageSize).ToList();

			return paginatedEmployees;
		}
		public async Task<IEnumerable<ShopEmployee>> GetEmployeesByDateOfBirthAsync(DateTime date)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.DateOfBirth == date).ToList();
		}
		public async Task<IEnumerable<ShopEmployee>> GetEmployeesByProfessionAsync(string professionName)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Proffession.Name == professionName).ToList();
		}
		public async Task<ShopEmployee?> GetByNameAsync(string fullName)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Name + se.Surname == fullName)
				.FirstOrDefault();	
		}
		public async Task<ShopEmployee?> GetByEmail(string email)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Email == email)
				.FirstOrDefault();
		}
		public async Task<ShopEmployee?> GetByIdAsync(string id)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Id == id)
				.FirstOrDefault();
		}
		public async Task<ShopEmployee?> GetByPhoneAsync(string phone)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.PhoneNumber == phone)
				.FirstOrDefault();
		}

		public async Task CreateAsync(ShopEmployee employee)
        {
			await _context.ShopEmployees.AddAsync(employee);
        }
		public void Update(ShopEmployee employee)
		{
			_context.ShopEmployees.Update(employee);
		}
		public async Task DeleteAsync(string id)
        {
			var employee = await GetByIdAsync(id);
			if(employee != null)
				_context.ShopEmployees.Remove(employee);
        }
    }
}
