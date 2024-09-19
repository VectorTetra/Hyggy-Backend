using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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
			return await _context.ShopEmployees.ToListAsync();
			//.Include(se => se.Proffession)
			//.Include(se => se.Shop).ToListAsync();
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
			//return employees.Where(se => se.Proffession.Name == professionName).ToList();
			return employees;
		}
		public async Task<IEnumerable<ShopEmployee>> GetBySurnameAsync(string surname)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Surname == surname)
				.ToList();	
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
			ShopEmployee? shopEmployee = _context.ShopEmployees.Where(s => s.Id == employee.Id).FirstOrDefault();
			shopEmployee.Surname = employee.Surname;
			shopEmployee.Name = employee.Name;
			shopEmployee.Email = employee.Email;
			shopEmployee.PhoneNumber = employee.PhoneNumber;
			shopEmployee.DateOfBirth = employee.DateOfBirth;
			shopEmployee.ShopId = employee.ShopId;
		}
		public async Task DeleteAsync(string id)
        {
			var employee = await GetByIdAsync(id);
			if(employee != null)
				_context.ShopEmployees.Remove(employee);
        }
    }
}
