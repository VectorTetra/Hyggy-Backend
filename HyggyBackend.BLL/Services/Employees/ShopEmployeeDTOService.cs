using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HyggyBackend.BLL.Services.Employees
{
	public class ShopEmployeeDTOService : IEmployeeService<ShopEmployeeDTO>
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
		private readonly IShopService _shopService;
		private readonly IEmailSender _emailSender;
        public ShopEmployeeDTOService(IUnitOfWork database, IMapper mapper, 
			UserManager<User> userManager, ITokenService tokenService, 
			 IShopService shopService,
			 IEmailSender emailSender)
		{
			Database = database;
			_mapper = mapper;
			_userManager = userManager;
			_tokenService = tokenService;
			_shopService = shopService;
			_emailSender = emailSender;
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
        public async Task<IEnumerable<ShopEmployeeDTO>?> GetBySurnameAsync(string surname)
        {
			var employee = await Database.ShopEmployees.GetBySurnameAsync(surname);
			return _mapper.Map<IEnumerable<ShopEmployeeDTO>>(employee);
		}
        public async Task<ShopEmployeeDTO?> GetByPhoneAsync(string phone)
        {
			var employee = await Database.ShopEmployees.GetByPhoneAsync(phone);
			return _mapper.Map<ShopEmployeeDTO>(employee);
		}
		
		public async Task<IEnumerable<ShopEmployeeDTO>> GetEmployeesByWorkPlaceId(long id)
		{
			var shop = await Database.Shops.GetById(id);
			if (shop == null)
				return null;

			var employees = shop.ShopEmployees.ToList();

			return _mapper.Map<IEnumerable<ShopEmployeeDTO>>(employees);
		}
		public async Task<RegistrationResponseDto> CreateAsync(EmployeeForRegistrationDto registrationDto)
		{
			var user = _mapper.Map<ShopEmployee>(registrationDto);

			var result = await _userManager.CreateAsync(user, registrationDto.Password!);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);

				return new RegistrationResponseDto { IsSuccessfullRegistration = false, Errors = errors };
			}
			await _userManager.AddToRoleAsync(user, registrationDto.Role!);
			
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var param = new Dictionary<string, string?>
			{
				{"token", token},
				{"email", user.Email }
			};

			registrationDto.UserUri = "http://localhost:5263/api/employee/emailconfirmation";
			var callback = QueryHelpers.AddQueryString(registrationDto.UserUri, param);


			var emailTemplate = EmailRegistrationTemplate(user.Name, callback);
			var message = new Message([user.Email], "Ласкаво просимо в команду Hyggy", emailTemplate);

			_emailSender.SendEmail(message);

			return new RegistrationResponseDto { IsSuccessfullRegistration = true };
			
		}
		public async Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto)
		{
			var user = await _userManager.FindByNameAsync(authenticationDto.Email!);
			if (user is null)
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірне ім'я" };

			if (!await _userManager.IsEmailConfirmedAsync(user))
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Активуйте свій обліковий запис" };

			if(!await _userManager.CheckPasswordAsync(user, authenticationDto.Password!))
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірний пароль" };

			var token = await _tokenService.CreateToken(user);

			return new AuthResponseDto { IsAuthSuccessfull = true, Token = token };
		}
		public async Task<string> EmailConfirmation(string email, string token)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null)
				throw new ValidationException("Пошту не знайдено", email);


			var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
			if(!confirmResult.Succeeded)
				throw new ValidationException("Пошту не знайдено", email);


			return "Обліковий запис підтвержено!";
		}
		public void Update(ShopEmployeeDTO shopEmployee)
		{
			var employee = _mapper.Map<ShopEmployee>(shopEmployee);
			Database.ShopEmployees.Update(employee);
			Database.Save();
		}
		public async Task DeleteAsync(string id)
		{
			await Database.ShopEmployees.DeleteAsync(id);
			await Database.Save();
		}

		private string EmailRegistrationTemplate(string name, string callback)
		{

			var template = $@"
				<!DOCTYPE html>
				<html lang='en'>
				<head>
					<meta charset='UTF-8'>
					<meta name='viewport' content='width=device-width, initial-scale=1.0'>
				<style>
					html{{
						width: 800px;
					}}
					body{{
						background-color: #F8F8F8;
					}}
					.container{{
						margin: 60px;
						margin-top: 40px;
					}}
					.innercontainer{{
						background-color: white;
					}}
					.maincontainer{{
						padding: 10px 20px;
					}}
					header{{
						display: flex;
						position: relative;
						justify-content: space-between;
					}}
					header>h1{{
						background-color: #143C8A;
						color: white;
						padding: 2px;
					}}
					header>h3{{
						position: absolute;
						right: 0;
						bottom: 0;
						color:gray;
						font-weight: 100;
					}}
					.reference{{
						display: flex;
						justify-content: center;
						border:1px gray solid;
						height: 50px;
					}}
					.reference>a{{
						font-size: large;
						font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
					}}
					.title{{
						display: flex;
						flex-direction: column;
						text-align: end;
					}}
					.title>h2{{
						margin-top: 50px;
						margin-bottom: 0;
					}}
					.title>h4{{
						margin-top: 0;
						font-weight: 100;
					}}
					p{{	
						font-size: 18px;
						font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
						font-weight:400;
					}}
					.buttonlink{{
					     display: inline-block;
						 padding: 10px 20px;
						 background-color: #143C8A;
						 text-decoration: none;
						 border-radius: 5px;
						 margin-top: 20px;
						 font-weight:bolder;
						 color: white;
					}}
					.buttonlink>p{{
						margin: auto;    
						

					}}
					footer{{
						margin-top: 50px;
						padding: 10px 20px;
						font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
					}}
				</style>
				</head>
				<body>
					<div class='container'>
					<header>
						<h1>Hyggy</h1>
						<h3>Відділ по роботі з клієнтами</h3>
					</header>
					<div class='innercontainer'>
					<main>
						<div class='reference'>
							<a href='#'>Перейти на сайт Hyggy.ua</a>
						</div>
						<div class='maincontainer'>
							<div class='title'>
								<h2>Ласкаво просимо на Hyggy.ua</h2>
								<h4>{DateTime.Now}</h4>
							</div>
							<div>
								<p>Вітаємо {name}<br/><br/>Ми раді вітати вас в команді Hyggy.ua. Все що вам потрібно - це активувати свій обліковий запис!
								<br/><br/>Зверніть увагу, що посилання активне лише 48 годин.<br/><br/>
								<a class='buttonlink' href='{callback}'>Активувати обліковий запис</a>
							 </div>
						</div>
					</main>
					<footer>
						<h3>ВІДДІЛ ПО РОБОТІ З КЛІЄНТАМИ</h3>
						<p>
							У Вас виникли запитання чи потрібна допомога? <a href='#'>Зверніться до Відділу по роботі з клієнтами</a>
						</p>
						<a href='#'>
							Адреса та години роботи магазину
						</a>
						<p>З повагою,<br/><span style='color: #143C8A;font-weight: bolder;'>Hyggy</span></p>
					</footer>
					</div>
					</div>
				</body>
				</html>";

			return template;

		}

	}
}
