﻿using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories.Employes
{
	public class StorageEmployeeRepository : IEmployeeRepository<StorageEmployee>
	{
		private readonly HyggyContext _context;
		public StorageEmployeeRepository (HyggyContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<StorageEmployee>> GetAllAsync()
		{
			return await _context.StorageEmployees
				//.Include(se => se.Proffession)
				.Include(se => se.Storage).ToListAsync();
		}
		public async Task<IEnumerable<StorageEmployee>> GetPaginatedEmployeesAsync(int? page)
		{
			const int pageSize = 10;
			var employees = await GetAllAsync();
			var paginatedEmployees = employees.Skip((page ?? 0) * pageSize)
				.Take(pageSize).ToList();

			return paginatedEmployees;
		}
		public async Task<IEnumerable<StorageEmployee>> GetEmployeesByDateOfBirthAsync(DateTime date)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.DateOfBirth == date).ToList();
		}
		public async Task<IEnumerable<StorageEmployee>> GetEmployeesByProfessionAsync(string professionName)
		{
			var employees = await GetAllAsync();
			//return employees.Where(se => se.Proffession.Name == professionName).ToList();
			return employees;
		}
		public async Task<StorageEmployee?> GetByNameAsync(string fullName)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Name + se.Surname == fullName)
				.FirstOrDefault();
		}
		public async Task<StorageEmployee?> GetByEmail(string email)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Email == email)
				.FirstOrDefault();
		}
		public async Task<StorageEmployee?> GetByIdAsync(string id)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.Id == id)
				.FirstOrDefault();
		}
		public async Task<StorageEmployee?> GetByPhoneAsync(string phone)
		{
			var employees = await GetAllAsync();
			return employees.Where(se => se.PhoneNumber == phone)
				.FirstOrDefault();
		}

        public async IAsyncEnumerable<StorageEmployee> GetByIdsAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var item = await GetByIdAsync(id);  // Виклик методу репозиторію
                if (item != null)
                {
                    yield return item;
                }
            }
        }
        public async Task CreateAsync(StorageEmployee employee)
		{
			await _context.StorageEmployees.AddAsync(employee);
		}
		public void Update(StorageEmployee employee)
		{
			StorageEmployee? storageEmployee = _context.StorageEmployees.Where(s => s.Id == employee.Id).FirstOrDefault();
			storageEmployee.Surname = employee.Surname;
			storageEmployee.Name = employee.Name;
			storageEmployee.Email = employee.Email;
			storageEmployee.PhoneNumber = employee.PhoneNumber;
			storageEmployee.DateOfBirth = employee.DateOfBirth;
			storageEmployee.StorageId = employee.StorageId;
		}
		public async Task DeleteAsync(string id)
		{
			var employee = await GetByIdAsync(id);
			if (employee != null)
				_context.StorageEmployees.Remove(employee);
		}

		public Task<IEnumerable<StorageEmployee>> GetBySurnameAsync(string surname)
		{
			throw new NotImplementedException();
		}
	}
}
