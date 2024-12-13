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
using System.Runtime.CompilerServices;

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

        public async Task<CustomerDTO> CreateOrFindGuestCustomerAsync(CustomerDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ValidationException("Не вказано ім'я гостьового клієнта для створення!","");
            if (string.IsNullOrWhiteSpace(item.Surname))
                throw new ValidationException("Не вказано прізвище гостьового клієнта для створення!", "");
            if (string.IsNullOrWhiteSpace(item.Email))
                throw new ValidationException("Не вказано email гостьового клієнта для створення!", "");
            if (string.IsNullOrWhiteSpace(item.PhoneNumber))
                throw new ValidationException("Не вказано телефон гостьового клієнта для створення!", "");

            var checkIfExistedByEmail = await GetByEmailSubstring(item.Email);
            if (checkIfExistedByEmail.Any(x => x.Email == item.Email))
            {
                var ExistedGuestToReturn = checkIfExistedByEmail.First(x => x.Email == item.Email);
                return ExistedGuestToReturn;
            }

            // Створення нового клієнта
            var newCustomer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = item.Name,
                Surname = item.Surname,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber
            };

            var creationResult = await _userManager.CreateAsync(newCustomer);
            if (!creationResult.Succeeded)
            {
                var errors = string.Join(", ", creationResult.Errors.Select(e => e.Description));
                throw new ValidationException($"Помилка при створенні гостьового клієнта: {errors}","");
            }

            // Додавання ролі
            await _userManager.AddToRoleAsync(newCustomer, "Guest");

            return _mapper.Map<Customer, CustomerDTO>(newCustomer);
        }


        public async Task<RegistrationResponseDto> RegisterAsync(UserForRegistrationDto registrationDto)
        {
            // Перевірка існування користувача з таким email
            var existingUser = await _userManager.FindByEmailAsync(registrationDto.Email!);
            if (existingUser != null)
            {
                // Перевіряємо, чи є цей користувач гостьовим
                var isGuest = await _userManager.IsInRoleAsync(existingUser, "Guest");
                if (isGuest)
                {
                    // Видаляємо гостьового користувача
                    var deletionResult = await _userManager.DeleteAsync(existingUser);
                    if (!deletionResult.Succeeded)
                    {
                        var errors = deletionResult.Errors.Select(e => e.Description);
                        return new RegistrationResponseDto
                        {
                            IsSuccessfullRegistration = false,
                            Errors = new[] { "Не вдалося видалити акаунт гостьового користувача: " + string.Join(", ", errors) }
                        };
                    }
                }
                else
                {
                    // Якщо користувач не гостьовий, повертаємо помилку
                    return new RegistrationResponseDto
                    {
                        IsSuccessfullRegistration = false,
                        Errors = new[] { "Користувач із таким email вже зареєстрований." }
                    };
                }
            }


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
            if (item.Id == null)
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

            // Оновлюємо колекцію улюблених товарів, якщо масив FavoriteWareIds не порожній
            if (item.FavoriteWareIds != null)
            {
                if (!item.FavoriteWareIds.Any())
                {
                    customer.FavoriteWares.Clear();  // Очищаємо колекцію, якщо масив порожній
                }
                else
                {
                    customer.FavoriteWares.Clear();  // Очищаємо старі дані
                    await foreach (var favWare in Database.Wares.GetByIdsAsync(item.FavoriteWareIds))
                    {
                        if (favWare == null)
                        {
                            throw new ValidationException($"Один з улюблених товарів не знайдено!", "");
                        }
                        customer.FavoriteWares.Add(favWare);  // Додаємо нові товари
                    }
                }
            }

            // Оновлюємо колекцію замовлень, якщо масив OrderIds не порожній
            if (item.OrderIds != null)
            {
                if (!item.OrderIds.Any())
                {
                    customer.Orders.Clear();  // Очищаємо колекцію, якщо масив порожній
                }
                else
                {
                    customer.Orders.Clear();  // Очищаємо старі замовлення
                    await foreach (var order in Database.Orders.GetByIdsAsync(item.OrderIds))
                    {
                        if (order == null)
                        {
                            throw new ValidationException($"Одне з замовлень не знайдено!", "");
                        }
                        customer.Orders.Add(order);  // Додаємо нові замовлення
                    }
                }
            }



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
                                            Ми раді вітати вас на JYSK.ua. Все що вам потрібно - це активувати свій обліковий запис!<br/><br/>
                                            Зверніть увагу, що посилання активне лише 48 годин.<br/><br/>
                                            <a href=""{callback}"" style=""display: inline-block; padding: 10px 20px; background-color: #00AAAD; color: white; text-decoration: none; border-radius: 5px; font-weight: bolder; margin-top: 20px;"">Активувати обліковий запис</a><br/><br/>
                                            Для того, щоб зробити свої покупки максимально приємними просимо заповнити особисті дані у своєму обліковому записі.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan=""2"" style=""padding: 10px 20px; "">
                            <h3 style=""margin: 0;"">ВІДДІЛ ПО РОБОТІ З КЛІЄНТАМИ</h3>
                            <p style=""font-size: 18px; font-weight: 400; margin: 10px 0;"">
                                У Вас виникли запитання чи потрібна допомога? 
                                <a href=""#"" style=""text-decoration: none; color: #00AAAD;"">Зверніться до Відділу по роботі з клієнтами</a>
                            </p>
                            <a href=""#"" style=""text-decoration: none; color: #00AAAD;"">
                                Адреса та години роботи магазину
                            </a>
                            <p style=""margin-top: 20px; font-size: 18px;"">
                                З повагою,<br/>
                                <span style=""color: #00AAAD; font-weight: bolder;"">Hyggy</span>
                            </p>
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
