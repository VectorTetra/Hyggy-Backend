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
        Task<Customer?> GetByIdAsync(long id);
        Task<IEnumerable<Customer>> GetByQuery(CustomerQueryDAL query);

        Task CreateAsync(Customer item);
        void Update(Customer item);
        Task DeleteAsync(long id);
    }
}
