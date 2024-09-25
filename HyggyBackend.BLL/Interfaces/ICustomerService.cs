using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize);
        Task<IEnumerable<CustomerDTO>> GetByOrderId(long orderId);
        Task<IEnumerable<CustomerDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<CustomerDTO>> GetBySurnameSubstring(string surnameSubstring);
        Task<IEnumerable<CustomerDTO>> GetByEmailSubstring(string emailSubstring);
        Task<IEnumerable<CustomerDTO>> GetByPhoneSubstring(string phoneSubstring);
        Task<CustomerDTO?> GetByIdAsync(string id);
        Task<IEnumerable<CustomerDTO>> GetByQuery(CustomerQueryBLL query);

        Task<CustomerDTO> CreateAsync(CustomerDTO item);
        Task<CustomerDTO> Update(CustomerDTO item);
        Task DeleteAsync(string id);
    }
}
