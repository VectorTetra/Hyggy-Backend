using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace HyggyBackend.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        IUnitOfWork Database;
        IMapper _mapper;
        ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;

        public CustomerService(IUnitOfWork uow, IMapper mapper, ITokenService tokenService,
            IEmailSender emailSender, UserManager<User> userManager)
        {
            Database = uow;
            _mapper = mapper;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize)
        {
            var customers = await Database.Customers.GetPagedCustomers(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByStringIds(string StringIds)
        {
            var customers = await Database.Customers.GetByStringIds(StringIds);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByOrderId(long orderId)
        {
            var customers = await Database.Customers.GetByOrderId(orderId);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByNameSubstring(string nameSubstring)
        {
            var customers = await Database.Customers.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetBySurnameSubstring(string surnameSubstring)
        {
            var customers = await Database.Customers.GetBySurnameSubstring(surnameSubstring);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByEmailSubstring(string emailSubstring)
        {
            var customers = await Database.Customers.GetByEmailSubstring(emailSubstring);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByPhoneSubstring(string phoneSubstring)
        {
            var customers = await Database.Customers.GetByPhoneSubstring(phoneSubstring);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<CustomerDTO?> GetByIdAsync(string id)
        {
            var customer = await Database.Customers.GetByIdAsync(id);
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByQuery(CustomerQueryBLL query)
        {
            var queryDAL = _mapper.Map<CustomerQueryBLL, CustomerQueryDAL>(query);
            var customers = await Database.Customers.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }

        //      public async Task<CustomerDTO> CreateAsync(CustomerDTO item) 
        //     {

        //         var customer = _mapper.Map<CustomerDTO, Customer>(item);
        //         await Database.Customers.CreateAsync(customer);
        //         await Database.Save();
        //var token = await _tokenService.CreateToken(customer);
        //         item.Token = token;
        //item.Id = customer.Id;
        //         return item;
        //     }

        public async Task<RegistrationResponseDto> RegisterAsync(UserForRegistrationDto registrationDto)
        {
            var user = _mapper.Map<Customer>(registrationDto);
            var result = await _userManager.CreateAsync(user, registrationDto.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return new RegistrationResponseDto { IsSuccessfullRegistration = false, Errors = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                { "token", token },
                {"email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(registrationDto.UserUri, param);
            var emailTemplate = EmailRegistrationTemplate(user.Name, callback);
            var message = new Message([user.Email!], "Ласкаво просимо на Hyggy", emailTemplate);
            _emailSender.SendEmail(message);

            return new RegistrationResponseDto { IsSuccessfullRegistration = true, EmailToken = token };
        }
        public async Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto)
        {
            var user = await _userManager.FindByNameAsync(authenticationDto.Email!);
            if (user is null)
                return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірне ім'я" };

            if (!await _userManager.CheckPasswordAsync(user, authenticationDto.Password!))
                return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірний пароль" };

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Активуйте свій обліковий запис" };

            var token = await _tokenService.CreateToken(user);

            return new AuthResponseDto { IsAuthSuccessfull = true, Token = token };
        }
        public async Task<string> EmailConfirmation(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new ValidationException("Пошту не знайдено", email);

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                throw new ValidationException("Пошту не підтвержено", email);


            return "Обліковий запис підтвержено!";
        }
        public async Task<CustomerDTO> Update(CustomerDTO item)
        {
            //var customer = _mapper.Map<CustomerDTO, Customer>(item);
            if(item.Id == null)
            {
                throw new ValidationException("Не вказано Id клієнта для оновлення!", "");
            }
            var customer = await Database.Customers.GetByIdAsync(item.Id);
            if (customer == null)
            {
                throw new ValidationException("Клієнт не знайдений!", item.Id);
            }
            if (item.Name == null)
            {
                throw new ValidationException("Не вказано ім'я клієнта для оновлення!", "");
            }
            if (item.Surname == null)
            {
                throw new ValidationException("Не вказано прізвище клієнта для оновлення!", "");
            }
            if (item.Email == null)
            {
                throw new ValidationException("Не вказано email клієнта для оновлення!", "");
            }
            customer.Name = item.Name;
            customer.Surname = item.Surname;
            customer.Email = item.Email;
            customer.PhoneNumber = item.PhoneNumber;
            customer.AvatarPath = item.AvatarPath;
            Database.Customers.Update(customer);
            await Database.Save();

            var returnedDTO = await GetByIdAsync(customer.Id);
            return returnedDTO;
        }
        public async Task DeleteAsync(string id)
        {
            await Database.Customers.DeleteAsync(id);
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
								<p>Вітаємо {name}<br/><br/>Ми раді вітати вас на Hyggy.ua. Все що вам потрібно - це активувати свій обліковий запис!
								<br/><br/>Зверніть увагу, що посилання активне лише 48 годин.<br/><br/>
								<a class='buttonlink' href='{callback}'>Активувати обліковий запис</a>
								<br/><br/>Для того, щоб зробити свої покупки максимально приємними просимо заповнити особисті дані у своєму обліковому записі.</p>
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
