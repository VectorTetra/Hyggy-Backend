using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.BLL.Services.Employees
{
    public class ShopEmployeeDTOService : IEmployeeService<ShopEmployeeDTO>
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
		private readonly SignInManager<User> _signInManager;
        public ShopEmployeeDTOService(IUnitOfWork database, IMapper mapper, UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
		{
			Database = database;
			_mapper = mapper;
			_userManager = userManager;
			_tokenService = tokenService;
			_signInManager = signInManager;
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
		public async Task<ShopEmployeeDTO?> GetByIdAsync(string id)
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
        public async Task<string> CreateAsync(RegisterDto shopEmployee)
        {
				var emloyeeDto = new ShopEmployeeDTO
				{
					UserName = shopEmployee.UserName,
					Email = shopEmployee.Email,
				};

				var employee = _mapper.Map<ShopEmployee>(emloyeeDto);
				var createdUser = await _userManager.CreateAsync(employee, shopEmployee.Password);
			    
				if (createdUser.Succeeded)
				{
					var roleResult = await _userManager.AddToRoleAsync(employee, "User");
					var token = _tokenService.CreateToken(employee);
					return token;
				}

			// await Database.ShopEmployees.CreateAsync(employee);
			return "Співробітник не создан";

		}
		public void Update(ShopEmployeeDTO shopEmployee)
		{
			var employee = _mapper.Map<ShopEmployee>(shopEmployee);

			Database.ShopEmployees.Update(employee);
		}
		public async Task DeleteAsync(string id)
        {
            await Database.ShopEmployees.DeleteAsync(id);
        }
       

		public async Task<ShopEmployeeDTO> Login(LoginDto login)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(x =>  x.UserName == login.UserName.ToLower());

			var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
			if(result.Succeeded)
			{
				var userDto = _mapper.Map<ShopEmployeeDTO>(user);
				userDto.Token = _tokenService.CreateToken(user);
				return userDto;
			}


			return null;
		}
    }
}
