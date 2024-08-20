using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;

namespace HyggyBackend.BLL.Services.Employees
{
	public class StorageEmployeeDTOService : IEmployeeService<StorageEmployeeDTO>
	{
		IUnitOfWork Database { get; set; }
		private readonly IMapper _mapper;

		public StorageEmployeeDTOService(IUnitOfWork database, IMapper mapper)
		{
			Database = database;
			_mapper = mapper;
		}

		public async Task<IEnumerable<StorageEmployeeDTO>> GetAllAsync()
		{
			var employees = await Database.StorageEmployees.GetAllAsync();
			return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
		}
		public async Task<IEnumerable<StorageEmployeeDTO>> GetPaginatedEmployeesAsync(int? page)
		{
			var paginatedEmployees = await Database.StorageEmployees.GetPaginatedEmployeesAsync(page);
			return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(paginatedEmployees);
		}
		public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByDateOfBirthAsync(DateTime date)
		{
			var employees = await Database.StorageEmployees.GetEmployeesByDateOfBirthAsync(date);
			return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
		}
		public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByProfessionAsync(string professionName)
		{
			var employees = await Database.StorageEmployees.GetEmployeesByProfessionAsync(professionName);
			return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
		}
		public async Task<StorageEmployeeDTO?> GetByIdAsync(string id)
		{
			var employee = await Database.StorageEmployees.GetByIdAsync(id);
			return _mapper.Map<StorageEmployeeDTO>(employee);
		}
		public async Task<StorageEmployeeDTO?> GetByEmail(string email)
		{
			var employee = await Database.StorageEmployees.GetByEmail(email);
			return _mapper.Map<StorageEmployeeDTO>(employee);
		}
		public async Task<StorageEmployeeDTO?> GetByNameAsync(string name)
		{
			var employee = await Database.StorageEmployees.GetByNameAsync(name);
			return _mapper.Map<StorageEmployeeDTO>(employee);
		}
		public async Task<StorageEmployeeDTO?> GetByPhoneAsync(string phone)
		{
			var employee = await Database.StorageEmployees.GetByPhoneAsync(phone);
			return _mapper.Map<StorageEmployeeDTO>(employee);
		}
		public async Task<string> CreateAsync(RegisterDto storageEmployee)
		{
			var employee = _mapper.Map<StorageEmployee>(storageEmployee);
			await Database.StorageEmployees.CreateAsync(employee);
			return string.Empty;
		}
		public void Update(StorageEmployeeDTO storageEmployee)
		{
			var employee = _mapper.Map<StorageEmployee>(storageEmployee);

			Database.StorageEmployees.Update(employee);
		}
		public async Task DeleteAsync(string id)
		{
			await Database.StorageEmployees.DeleteAsync(id);
		}

		public Task<ShopEmployeeDTO> Login(LoginDto login)
		{
			throw new NotImplementedException();
		}
	}
}
