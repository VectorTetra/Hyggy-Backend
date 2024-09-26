using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        IUnitOfWork Database;
        IMapper _mapper;
        public CustomerService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize)
        {
            var customers = await Database.Customers.GetPagedCustomers(pageNumber, pageSize);
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

        public async Task<CustomerDTO> CreateAsync(CustomerDTO item) 
        {

            var customer = _mapper.Map<CustomerDTO, Customer>(item);
            await Database.Customers.CreateAsync(customer);
            await Database.Save();

            item.Id = customer.Id;
            return item;
        }
        public async Task<CustomerDTO> Update(CustomerDTO item) 
        {
            var customer = _mapper.Map<CustomerDTO, Customer>(item);
            Database.Customers.Update(customer);
            await Database.Save();

            var returnedDTO = await GetByIdAsync(customer.Id);
            return returnedDTO;
        }
        public async Task DeleteAsync(string id) 
        {
            await Database.Customers.DeleteAsync(id);
        }

    }
}
