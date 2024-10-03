using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly HyggyContext _context;

        public CustomerRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetPagedCustomers(int pageNumber, int pageSize)
        {
            return await _context.Customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetByOrderId(long orderId)
        {
            return await _context.Customers.Where(x => x.Orders.Any(o => o.Id == orderId)).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.Customers.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetBySurnameSubstring(string surnameSubstring)
        {
            return await _context.Customers.Where(x => x.Surname.Contains(surnameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetByEmailSubstring(string emailSubstring)
        {
            return await _context.Customers.Where(x => x.Email.Contains(emailSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetByPhoneSubstring(string phoneSubstring)
        {
            return await _context.Customers.Where(x => x.PhoneNumber.Contains(phoneSubstring)).ToListAsync();
        }
        public async Task<Customer?> GetByIdAsync(string id)
        {
            return await _context.Customers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer>> GetByQuery(CustomerQueryDAL query)
        {
            var customerCollections = new List<IEnumerable<Customer>>();

            if (query.Id != null)
            {
                customerCollections.Add(await _context.Customers.Where(x => x.Id == query.Id).ToListAsync());
            }
            if (query.Name != null)
            {
                customerCollections.Add(await _context.Customers.Where(x => x.Name.Contains(query.Name)).ToListAsync());
            }
            if (query.Surname != null)
            {
                customerCollections.Add(await _context.Customers.Where(x => x.Surname.Contains(query.Surname)).ToListAsync());
            }
            if (query.Email != null)
            {
                customerCollections.Add(await _context.Customers.Where(x => x.Email.Contains(query.Email)).ToListAsync());
            }
            if (query.Phone != null)
            {
                customerCollections.Add(await _context.Customers.Where(x => x.PhoneNumber.Contains(query.Phone)).ToListAsync());
            }

            if (query.OrderId != null)
            {
                customerCollections.Add(await GetByOrderId(query.OrderId.Value));
            }

            if (!customerCollections.Any())
            {
                return new List<Customer>();
            }

            if (query.PageNumber != null && query.PageSize != null)
            {
                return customerCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value);
            }


            return customerCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }

        public async IAsyncEnumerable<Customer> GetByIdsAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var wareReview = await GetByIdAsync(id);  // Виклик методу репозиторію
                if (wareReview != null)
                {
                    yield return wareReview;
                }
            }
        }

        public async Task CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }
        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

        }
        public async Task DeleteAsync(string id)
        {
            Customer? customer = await _context.Customers.FindAsync(id);
            if (customer != null)
                _context.Customers.Remove(customer);
        }
    }
}
