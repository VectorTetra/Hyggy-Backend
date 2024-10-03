using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetPagedCustomers(int pageNumber, int pageSize);
        Task<IEnumerable<Customer>> GetByOrderId(long orderId);
        Task<IEnumerable<Customer>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<Customer>> GetBySurnameSubstring(string surnameSubstring);
        Task<IEnumerable<Customer>> GetByEmailSubstring(string emailSubstring);
        Task<IEnumerable<Customer>> GetByPhoneSubstring(string phoneSubstring);
        IAsyncEnumerable<Customer> GetByIdsAsync(IEnumerable<string> ids);
        Task<Customer?> GetByIdAsync(string id);
        Task<IEnumerable<Customer>> GetByQuery(CustomerQueryDAL query);

        Task CreateAsync(Customer item);
        void Update(Customer item);
        Task DeleteAsync(string id);
    }
}
