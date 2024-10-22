using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
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

        public async Task<IEnumerable<Customer>> GetByStringIds(string StringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список string
            List<string> ids = StringIds.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();

            // Створюємо список для збереження результатів
            var customers = new List<Customer>();

            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var customer in GetByIdsAsync(ids))
            {
                customers.Add(customer);
            }

            return customers;
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
            var collections = new List<IEnumerable<Customer>>();

            if (query.QueryAny != null)
            {
                collections.Add(await GetByNameSubstring(query.QueryAny));
                collections.Add(await GetBySurnameSubstring(query.QueryAny));
                collections.Add(await GetByEmailSubstring(query.QueryAny));
                collections.Add(await GetByPhoneSubstring(query.QueryAny));
                collections.Add(new List<Customer> { await GetByIdAsync(query.QueryAny) });
            }
            else
            {
                if (query.Id != null)
                {
                    var res = await GetByIdAsync(query.Id);
                    if (res != null)
                    {
                        collections.Add(new List<Customer> { res });
                    }
                }
                if (query.Name != null)
                {
                    collections.Add(await GetByNameSubstring(query.Name));
                }
                if (query.Surname != null)
                {
                    collections.Add(await GetBySurnameSubstring(query.Surname));
                }
                if (query.Email != null)
                {
                    collections.Add(await GetByEmailSubstring(query.Email));
                }
                if (query.Phone != null)
                {
                    collections.Add(await GetByPhoneSubstring(query.Phone));
                }
                if (query.OrderId != null)
                {
                    var res = await GetByOrderId(query.OrderId.Value);
                    if (res != null)
                    {
                        collections.Add( res );
                    }
                }
                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<Customer>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Customers
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }

            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "NameAsc":
                        result = result.OrderBy(x => x.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "SurnameAsc":
                        result = result.OrderBy(x => x.Surname).ToList();
                        break;
                    case "SurnameDesc":
                        result = result.OrderByDescending(x => x.Surname).ToList();
                        break;
                    case "EmailAsc":
                        result = result.OrderBy(x => x.Email).ToList();
                        break;
                    case "EmailDesc":
                        result = result.OrderByDescending(x => x.Email).ToList();
                        break;
                    case "PhoneAsc":
                        result = result.OrderBy(x => x.PhoneNumber).ToList();
                        break;
                    case "PhoneDesc":
                        result = result.OrderByDescending(x => x.PhoneNumber).ToList();
                        break;
                    case "IdAsc":
                        result = result.OrderBy(x => x.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(x => x.Id).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<Customer>();
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
