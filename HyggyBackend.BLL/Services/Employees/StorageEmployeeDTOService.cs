﻿using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.DTO.EmployeesDTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.BLL.Services.EmailService;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace HyggyBackend.BLL.Services.Employees
{
    public class StorageEmployeeDTOService : IEmployeeService<StorageEmployeeDTO>
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public StorageEmployeeDTOService(IUnitOfWork database, IMapper mapper,
            UserManager<User> userManager, ITokenService tokenService,IEmailSender emailSender, IConfiguration configuration)
        {
            Database = database;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public async Task<IEnumerable<StorageEmployeeDTO>> GetAllAsync()
        {
            var employees = await Database.StorageEmployees.GetAllAsync();

            return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>> GetPaged(int pageNumber, int PageSize)
        {
            var paginatedEmployees = await Database.StorageEmployees.GetPaged(pageNumber, PageSize);
            return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(paginatedEmployees);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByWorkPlaceId(long id)
        {
            var storage = await Database.Storages.GetById(id);
            if (storage == null)
                return null;

            var employees = storage.StorageEmployees.ToList();

            return _mapper.Map<IEnumerable<StorageEmployeeDTO>>(employees);

        }
        public async Task<IEnumerable<StorageEmployeeDTO>> GetEmployeesByDateOfBirth(DateTime date)
        {
            var employees = await Database.StorageEmployees.GetEmployeesByDateOfBirth(date);
            return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>?> GetBySurname(string surname)
        {
            var employee = await Database.StorageEmployees.GetBySurname(surname);
            return _mapper.Map<IEnumerable<StorageEmployeeDTO>>(employee);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>> GetByName(string name)
        {
            var employees = await Database.StorageEmployees.GetByName(name);
            return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>> GetByStringIds(string stringIds)
        {
            var employees = await Database.StorageEmployees.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
        }
        public async Task<StorageEmployeeDTO?> GetById(string id)
        {
            var employee = await Database.StorageEmployees.GetById(id);
            return _mapper.Map<StorageEmployeeDTO>(employee);
        }
        public async Task<StorageEmployeeDTO?> GetByEmail(string email)
        {
            var employee = await Database.StorageEmployees.GetByEmail(email);
            return _mapper.Map<StorageEmployeeDTO>(employee);
        }
        public async Task<StorageEmployeeDTO?> GetByPhoneNumber(string phone)
        {
            var employee = await Database.StorageEmployees.GetByPhoneNumber(phone);
            return _mapper.Map<StorageEmployeeDTO>(employee);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>?> GetByRoleName(string roleName)
        {
            var employee = await Database.StorageEmployees.GetByRoleName(roleName);
            return _mapper.Map<IEnumerable<StorageEmployeeDTO>>(employee);
        }
        public async Task<string?> GetRoleName(string employeeId)
        {
            return await Database.StorageEmployees.GetRoleName(employeeId);
        }
        public async Task<IEnumerable<StorageEmployeeDTO>> GetByQuery(EmployeeQueryBLL query)
        {
            var employees = await Database.StorageEmployees.GetByQuery(_mapper.Map<EmployeeQueryDAL>(query));
            var employeesDTOs = _mapper.Map<IEnumerable<StorageEmployee>, IEnumerable<StorageEmployeeDTO>>(employees);
            foreach (var employee in employeesDTOs)
            {
                employee.RoleName = await GetRoleName(employee.Id);
            }
            return employeesDTOs;
        }
        public async Task<RegistrationResponseDto> Create(EmployeeForRegistrationDto registrationDto)
        {
            var user = _mapper.Map<StorageEmployee>(registrationDto);

            var result = await _userManager.CreateAsync(user, registrationDto.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return new RegistrationResponseDto { IsSuccessfullRegistration = false, Errors = errors };
            }

            await _userManager.AddToRoleAsync(user, registrationDto.RoleName!);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token},
                {"email", user.Email }
            };

            registrationDto.UserUri = "https://www.hyggy.somee.com/api/storageemployee/emailconfirmation";

            var callback = QueryHelpers.AddQueryString(registrationDto.UserUri, param);
            var emailTemplate = EmailRegistrationTemplate(user.Name, callback);


            var message = new Message([user.Email], "Ласкаво просимо в команду Hyggy", emailTemplate);

            _emailSender.SendEmail(message);

            return new RegistrationResponseDto { IsSuccessfullRegistration = true };
        }
        public async Task<bool> EmailConfirmation(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new ValidationException("Пошту не знайдено", email);


            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                throw new ValidationException("Пошту не знайдено", email);


            return true;
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
        public async Task<StorageEmployeeDTO> Update(StorageEmployeeDTO storageEmployeeDTO)
        {
            try
            {
                StorageEmployee? storageEmployee = await Database.StorageEmployees.GetById(storageEmployeeDTO.Id);
                if (storageEmployee == null) throw new ValidationException("Співробітника з таким Id не знайдено", storageEmployeeDTO.Id.ToString());

                var storage = await Database.Storages.GetById(storageEmployeeDTO.StorageId);
                var existedStorage = storage ?? throw new ValidationException($"Склад з таким Id не знайдено! (Id: {storageEmployeeDTO.StorageId})", storageEmployeeDTO.StorageId.ToString());
                storageEmployee.StorageId = existedStorage.Id;

                storageEmployee.Surname = storageEmployeeDTO.Surname ?? throw new ValidationException($"Необхідно вказати прізвище співробітника! (Прізвище: {storageEmployeeDTO.Surname})", storageEmployeeDTO.Surname.ToString()); ;

                storageEmployee.Name = storageEmployeeDTO.Name ?? throw new ValidationException($"Необхідно вказати ім'я співробітника! (Ім'я: {storageEmployeeDTO.Name})", storageEmployeeDTO.Name.ToString()); ;

                storageEmployee.Email = storageEmployeeDTO.Email ?? throw new ValidationException($"Необхідно вказати пошту співробітника! (Пошта: {storageEmployeeDTO.Email})", storageEmployeeDTO.Email.ToString()); ;

                storageEmployee.PhoneNumber = storageEmployeeDTO.PhoneNumber;
                if (!string.IsNullOrEmpty(storageEmployeeDTO.OldPassword) && !string.IsNullOrEmpty(storageEmployeeDTO.NewPassword))
                {
                    var userEntity = await _userManager.FindByEmailAsync(storageEmployeeDTO.Email);

                    if (userEntity == null)
                        throw new ValidationException("Користувача з такою поштою не знайдено", storageEmployeeDTO.Email);

                    if (userEntity.Id != storageEmployeeDTO.Id)
                        throw new ValidationException("Користувач з такою поштою вже існує", storageEmployeeDTO.Email);

                    if (userEntity.Email != storageEmployeeDTO.Email)
                        throw new ValidationException("Пошта не може бути змінена", storageEmployeeDTO.Email);
                    var changePasswordResult = await _userManager.ChangePasswordAsync(userEntity, storageEmployeeDTO.OldPassword, storageEmployeeDTO.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        var errors = changePasswordResult.Errors.Select(e => e.Description);
                        throw new ValidationException("Помилка при зміні паролю", string.Join(", ", errors));
                    }
                }
                // Видаляємо всі ролі співробітника
                var currentRoles = await _userManager.GetRolesAsync(storageEmployee);
                if (currentRoles.Any(x => x != storageEmployeeDTO.RoleName))
                {
                    await _userManager.RemoveFromRolesAsync(storageEmployee, currentRoles);
                    await _userManager.AddToRoleAsync(storageEmployee, storageEmployeeDTO.RoleName);
                }

                Database.StorageEmployees.Update(storageEmployee);
                await Database.Save();

                return _mapper.Map<StorageEmployeeDTO>(storageEmployee);

            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message, ex.InnerException?.Message);
            }
        }
        public async Task Delete(string id)
        {
            await Database.StorageEmployees.Delete(id);
            await Database.Save();
        }
        private string EmailRegistrationTemplate(string name, string callback)
        {
            var frontendBaseUrl = _configuration["BaseUrls:Frontend"];
            var hyggyIconUrl = _configuration["BaseUrls:HyggyIcon"];
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
            <img src=""{hyggyIconUrl}"" style=""height: 70px; width: 120px;""/>
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
                            <a href=""{frontendBaseUrl}"" style=""color: #00AAAD; font-size: large; text-decoration: none; display: inline-block;"">Перейти на сайт Hyggy.ua</a>
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
