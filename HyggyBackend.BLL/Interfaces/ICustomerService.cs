using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<RegistrationResponseDto> RegisterAsync(UserForRegistrationDto registrationDto);
        Task<AuthResponseDto> AuthenticateAsync(UserForAuthenticationDto authenticationDto);
        Task<string> EmailConfirmation(string email, string code);
        Task<IEnumerable<CustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize);

        Task<IEnumerable<CustomerDTO>> GetByStringIds(string StringIds);
        Task<IEnumerable<CustomerDTO>> GetByOrderId(long orderId);
        Task<IEnumerable<CustomerDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<CustomerDTO>> GetBySurnameSubstring(string surnameSubstring);
        Task<IEnumerable<CustomerDTO>> GetByEmailSubstring(string emailSubstring);
        Task<IEnumerable<CustomerDTO>> GetByPhoneSubstring(string phoneSubstring);
        Task<CustomerDTO?> GetByIdAsync(string id);
        Task<IEnumerable<CustomerDTO>> GetByQuery(CustomerQueryBLL query);

        // Task<CustomerDTO> CreateAsync(CustomerDTO item);
        Task<CustomerDTO> Update(CustomerDTO item);
        Task DeleteAsync(string id);
    }
}
