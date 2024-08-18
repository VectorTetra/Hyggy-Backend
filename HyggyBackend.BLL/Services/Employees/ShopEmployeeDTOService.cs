using AutoMapper;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;

namespace HyggyBackend.BLL.Services.Employees
{
    public class ShopEmployeeDTOService : IEmployeeService<ShopEmployeeDTO>
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public ShopEmployeeDTOService(IUnitOfWork database, IMapper mapper)
        {
            Database = database;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ShopEmployeeDTO>> GetAllAsync()
        {
            var employees = await Database.ShopEmployees.GetAllAsync();
            return _mapper.Map<IEnumerable<ShopEmployee>, IEnumerable<ShopEmployeeDTO>>(employees);
        }
        public async Task<IEnumerable<ShopEmployeeDTO>> GetPaginatedEmployeesAsync(int? page)
        {
            var paginatedEmployees = await Database.ShopEmployees.GetPaginatedEmployeesAsync(page);
            return _mapper.Map<IEnumerable<ShopEmployee>, IEnumerable<ShopEmployeeDTO>>(paginatedEmployees);
        }
        public async Task<IEnumerable<ShopEmployeeDTO>> GetEmployeesByDateOfBirthAsync(DateTime date)
        {
            var employees = await Database.ShopEmployees.GetEmployeesByDateOfBirthAsync(date);
			return _mapper.Map<IEnumerable<ShopEmployee>, IEnumerable<ShopEmployeeDTO>>(employees);
		}
		public async Task<IEnumerable<ShopEmployeeDTO>> GetEmployeesByProfessionAsync(string professionName)
        {
            var employees = await Database.ShopEmployees.GetEmployeesByProfessionAsync(professionName);
			return _mapper.Map<IEnumerable<ShopEmployee>, IEnumerable<ShopEmployeeDTO>>(employees);
		}
		public async Task<ShopEmployeeDTO?> GetByIdAsync(long id)
		{
			var employee = await Database.ShopEmployees.GetByIdAsync(id);
			return _mapper.Map<ShopEmployeeDTO>(employee);
		}
		public async Task<ShopEmployeeDTO?> GetByEmail(string email)
        {
            var employee = await Database.ShopEmployees.GetByEmail(email);
            return _mapper.Map<ShopEmployeeDTO>(employee);
        }
        public async Task<ShopEmployeeDTO?> GetByNameAsync(string name)
        {
			var employee = await Database.ShopEmployees.GetByNameAsync(name);
			return _mapper.Map<ShopEmployeeDTO>(employee);
		}
        public async Task<ShopEmployeeDTO?> GetByPhoneAsync(string phone)
        {
			var employee = await Database.ShopEmployees.GetByPhoneAsync(phone);
			return _mapper.Map<ShopEmployeeDTO>(employee);
		}
        public async Task CreateAsync(ShopEmployeeDTO shopEmployee)
        {
            var employee = _mapper.Map<ShopEmployee>(shopEmployee);
            await Database.ShopEmployees.CreateAsync(employee);
        }
		public void Update(ShopEmployeeDTO shopEmployee)
		{
			var employee = _mapper.Map<ShopEmployee>(shopEmployee);

			Database.ShopEmployees.Update(employee);
		}
		public async Task DeleteAsync(long id)
        {
            await Database.ShopEmployees.DeleteAsync(id);
        }
       
    }
}
