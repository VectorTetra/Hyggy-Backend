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

namespace HyggyBackend.BLL.Services.Employees
{
	public class StorageEmployeeDTOService : IEmployeeService<StorageEmployeeDTO>
	{
		IUnitOfWork Database { get; set; }
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly ITokenService _tokenService;
		private readonly IShopService _shopService;
		private readonly IEmailSender _emailSender;
		public StorageEmployeeDTOService(IUnitOfWork database, IMapper mapper,
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

		public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByProfessionAsync(string professionName)
		{
			var employees = await Database.StorageEmployees.GetEmployeesByProfessionAsync(professionName);
            return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
        }

		public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByWorkPlaceId(long id)
		{
			var storage = await Database.Storages.GetById(id);
			if (storage == null)
				return null;

			var employees = storage.StorageEmployees.ToList();

			return _mapper.Map<IEnumerable<StorageEmployeeDTO>>(employees);

		}

		public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByDateOfBirthAsync(DateTime date)
		{
			var employees = await Database.StorageEmployees.GetEmployeesByDateOfBirthAsync(date);
			return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
		}
		public async Task<IEnumerable<StorageEmployeeDTO>?> GetBySurnameAsync(string surname)
		{
			var employee = await Database.StorageEmployees.GetBySurnameAsync(surname);
			return _mapper.Map<IEnumerable<StorageEmployeeDTO>>(employee);
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

		public async Task<StorageEmployeeDTO?> GetByPhoneAsync(string phone)
		{
			var employee = await Database.StorageEmployees.GetByPhoneAsync(phone);
			return _mapper.Map<StorageEmployeeDTO>(employee);
		}

		public async Task<RegistrationResponseDto> CreateAsync(EmployeeForRegistrationDto registrationDto)
		{
			var user = _mapper.Map<StorageEmployee>(registrationDto);

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

			registrationDto.UserUri = "http://www.hyggy.somee.com/api/storageemployee/emailconfirmation";

			var callback = QueryHelpers.AddQueryString(registrationDto.UserUri, param);
			var emailTemplate = EmailRegistrationTemplate(user.Name, callback);


			var message = new Message([user.Email], "Ласкаво просимо в команду Hyggy", emailTemplate);

			_emailSender.SendEmail(message);

			return new RegistrationResponseDto { IsSuccessfullRegistration = true };
		}

		public async Task<string> EmailConfirmation(string email, string token)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null)
				throw new ValidationException("Пошту не знайдено", email);


			var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
			if (!confirmResult.Succeeded)
				throw new ValidationException("Пошту не знайдено", email);


			return "Обліковий запис підтвержено!";
		}
		public async Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto)
		{
			var user = await _userManager.FindByNameAsync(authenticationDto.Email!);
			if (user is null)
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірне ім'я" };

			if (!await _userManager.IsEmailConfirmedAsync(user))
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Активуйте свій обліковий запис" };

			if (!await _userManager.CheckPasswordAsync(user, authenticationDto.Password!))
				return new AuthResponseDto { IsAuthSuccessfull = false, Error = "Невірний пароль" };

			var token = await _tokenService.CreateToken(user);

			return new AuthResponseDto { IsAuthSuccessfull = true, Token = token };
		}

		public void Update(StorageEmployeeDTO storageEmployee)
		{
			var employee = _mapper.Map<StorageEmployee>(storageEmployee);
			Database.StorageEmployees.Update(employee);
			Database.Save();
		}

		public async  Task DeleteAsync(string id)
		{
			await Database.StorageEmployees.DeleteAsync(id);
			await Database.Save();
		}


        private string EmailRegistrationTemplate(string name, string callback)
        {
            var template = $@"
                <!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
</head>
<body style=""background-color: #F8F8F8; margin: 60px; margin-top: 40px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; width: 800px;padding: 50px;"">
    <table style=""width: 100%; border-spacing: 0 20px;"">
        <tr>
            <td colspan=""2"" style=""padding: 0;"">
               <table style=""width: 100%; padding: 0; border-spacing: 0;"">
    <tr>
        <td style=""width: 50%;"">
            <img src=""https://s3-alpha-sig.figma.com/img/c71e/882e/8f5e30e3f0c3f65fa59a05f0a2b31601?Expires=1733097600&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=RC32EDo92P-pKnqdiA8XS0bgZVBmS2pcxK6xNkZD0eHbSZgC-clQm5JLRcIsPl7g2lVS59-NI4D2MAus68gHvWOCheMtkvf4eYQUGLUi0MI~03n7q3GhTiEusD6v-gtHr88iohFvE7OXgXbXqBo791X7MmLgawVkmrhZeGiHR16mf7MUmt~mzPTVtEtlxXvMlPsHGPm35Sx9Uly-U0~lHgBJfNqnEsCauWI~YDeRS36wghjpgzPSl7VLYt4bUVqRR9B6-EawazE-whMTcpvBndlHOWj-HyBmpqkM833KVJB24LeQ6LvKI0LVU25VsrrTJ9UtJN3oXOc6WH-JTTA8aw__"" style=""height: 70px; width: 120px;""/>
        </td>
        <td style=""width: 50%; text-align: right; vertical-align: bottom;"">
            <h3 style=""color: gray; font-weight: 100; margin: 0;"">Відділ по роботі з клієнтами</h3>
        </td>
    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td colspan=""2"" style=""background-color: white; padding: 0px;"">
                <table style=""width: 100%; border-spacing: 0;"">
                    <tr>
                        <td style=""text-align: center; border: 1px solid gray; height: 50px; padding: 0;"">
                            <a href=""http://localhost:3000/"" style=""color: #00AAAD; font-size: large; text-decoration: none; display: inline-block;"">Перейти на сайт Hyggy.ua</a>
                        </td>
                    </tr>
                    <tr >
                        <td style=""padding: 10px 20px;"">
                            <table style=""width: 100%; border-spacing: 0;"">
                                <tr>
                                    <td style=""text-align: right;"">
                                        <h2 style=""margin-top: 50px; margin-bottom: 0;"">Ласкаво просимо на Hyggy.ua</h2>
                                        <h4 style=""margin-top: 0; font-weight: 100;"">{DateTime.Now}</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style=""font-size: 18px; font-weight: 400;"">
                                            Вітаємо {name}<br/><br/>
                                            Ми раді вітати вас в команді Hyggy.ua. Все що вам потрібно - це активувати свій обліковий запис!<br/><br/>
                                            Зверніть увагу, що посилання активне лише 48 годин.<br/><br/>
                                            <a href=""{callback}"" style=""display: inline-block; padding: 10px 20px; background-color: #00AAAD; color: white; text-decoration: none; border-radius: 5px; font-weight: bolder; margin-top: 20px;"">Активувати обліковий запис</a><br/><br/>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
</body>
</html>
                ";
            return template;
        }

    }
}
