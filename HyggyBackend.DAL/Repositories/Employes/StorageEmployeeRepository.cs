using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories.Employes
{
    public class StorageEmployeeRepository : IEmployeeRepository<StorageEmployee>
    {
        private readonly HyggyContext _context;
        private readonly UserManager<User> _userManager;
        public StorageEmployeeRepository(HyggyContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IEnumerable<StorageEmployee>> GetAllAsync()
        {
            return await _context.StorageEmployees.ToListAsync();
            //.Include(se => se.Proffession)
            //.Include(se => se.Shop).ToListAsync();
        }
        public async Task<IEnumerable<StorageEmployee>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.StorageEmployees
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }
        public async Task<IEnumerable<StorageEmployee>> GetEmployeesByDateOfBirth(DateTime date)
        {
            var employees = await GetAllAsync();
            return employees.Where(se => se.DateOfBirth == date).ToList();
        }
        public async Task<IEnumerable<StorageEmployee>> GetBySurname(string surname)
        {
            var employees = await GetAllAsync();
            return employees.Where(se => se.Surname.Contains(surname))
                .ToList();
        }
        public async Task<IEnumerable<StorageEmployee>> GetByName(string name)
        {
            var employees = await GetAllAsync();
            return employees.Where(se => se.Name.Contains(name))
                .ToList();
        }
        public async Task<IEnumerable<StorageEmployee>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список string
            List<string> ids = stringIds.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();

            // Створюємо список для збереження результатів
            var StorageEmployees = new List<StorageEmployee>();

            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var customer in GetByIdsAsync(ids))
            {
                StorageEmployees.Add(customer);
            }

            return StorageEmployees;
        }
        public async Task<StorageEmployee?> GetByEmail(string email)
        {
            var employees = await GetAllAsync();
            return employees.Where(se => se.Email == email)
                .FirstOrDefault();
        }
        public async Task<StorageEmployee?> GetById(string id)
        {
            var employees = await GetAllAsync();
            return employees.Where(se => se.Id == id)
                .FirstOrDefault();
        }
        public async Task<StorageEmployee?> GetByPhoneNumber(string phone)
        {
            var employees = await GetAllAsync();
            return employees.Where(se => se.PhoneNumber == phone)
                .FirstOrDefault();
        }
        public async Task<IEnumerable<StorageEmployee>> GetByRoleName(string roleName)
        {
            var usersByRoleName = await _userManager.GetUsersInRoleAsync(roleName);
            var employees = await GetAllAsync();
            return employees.Where(se => usersByRoleName.Any(ubr => ubr.Id == se.Id));

        }
        public async Task<string?> GetRoleName(string employeeId)
        {
            var user = await _userManager.FindByIdAsync(employeeId);
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }
        public async IAsyncEnumerable<StorageEmployee> GetByIdsAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                var item = await GetById(id);  // Виклик методу репозиторію
                if (item != null)
                {
                    yield return item;
                }
            }
        }
        public async Task<IEnumerable<StorageEmployee>> GetByQuery(EmployeeQueryDAL query)
        {
            var collections = new List<IEnumerable<StorageEmployee>>();

            // Якщо вказано QueryAny, виконуємо запити за різними критеріями
            if (query.QueryAny != null)
            {
                //if (long.TryParse(query.QueryAny, out long id))
                //{
                //    collections.Add(new List<Shop> { await GetById(id) });
                //}
                collections.Add(new List<StorageEmployee> { await GetById(query.QueryAny) });
                collections.Add(new List<StorageEmployee> { await GetByPhoneNumber(query.QueryAny) });
                collections.Add(new List<StorageEmployee> { await GetByEmail(query.QueryAny) });
                collections.Add(await GetByName(query.QueryAny));
                collections.Add(await GetBySurname(query.QueryAny));
                collections.Add(await GetByRoleName(query.QueryAny));
                collections.Add(await GetByStringIds(query.QueryAny));
            }
            else
            {
                if (query.Id != null)
                {
                    var res = await GetById(query.Id);
                    if (res != null)
                    {
                        collections.Add(new List<StorageEmployee> { res });
                    }
                }
                if (query.Email != null)
                {
                    var res = await GetByEmail(query.Email);
                    if (res != null)
                    {
                        collections.Add(new List<StorageEmployee> { res });
                    }
                }
                if (query.PhoneNumber != null)
                {
                    var res = await GetByPhoneNumber(query.PhoneNumber);
                    if (res != null)
                    {
                        collections.Add(new List<StorageEmployee> { res });
                    }
                }

                if (query.Name != null)
                    collections.Add(await GetByName(query.Name));

                if (query.Surname != null)
                    collections.Add(await GetBySurname(query.Surname));

                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }
            var result = new List<StorageEmployee>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.StorageEmployees
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Використовуємо Union для об'єднання результатів
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                var nonEmptyCollections = collections.Where(collection => collection.Any()).ToList();

                // Перетин результатів з відфільтрованих колекцій
                if (nonEmptyCollections.Any())
                {
                    result = nonEmptyCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
                }
                else
                {
                    result = new List<StorageEmployee>(); // Повертаємо порожній список, якщо всі колекції були порожні
                }
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "NameAsc":
                        result = result.OrderBy(s => s.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(s => s.Name).ToList();
                        break;
                    case "SurnameAsc":
                        result = result.OrderBy(s => s.Surname).ToList();
                        break;
                    case "SurnameDesc":
                        result = result.OrderByDescending(s => s.Surname).ToList();
                        break;
                    case "EmailAsc":
                        result = result.OrderBy(s => s.Email).ToList();
                        break;
                    case "EmailDesc":
                        result = result.OrderByDescending(s => s.Email).ToList();
                        break;
                    case "PhoneNumberAsc":
                        result = result.OrderBy(s => s.PhoneNumber).ToList();
                        break;
                    case "PhoneNumberDesc":
                        result = result.OrderByDescending(s => s.PhoneNumber).ToList();
                        break;
                    case "DateOfBirthAsc":
                        result = result.OrderBy(s => s.DateOfBirth).ToList();
                        break;
                    case "DateOfBirthDesc":
                        result = result.OrderByDescending(s => s.DateOfBirth).ToList();
                        break;
                    case "IdAsc":
                        result = result.OrderBy(s => s.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(s => s.Id).ToList();
                        break;
                    case "RoleNameAsc":
                        result = result.OrderBy(async s => await GetRoleName(s.Id)).ToList();
                        break;
                    case "RoleNameDesc":
                        result = result.OrderByDescending(async s => await GetRoleName(s.Id)).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            result = result.Where(x => x != null).ToList();
            return result.Any() ? result : new List<StorageEmployee>();
        }
        public async Task Create(StorageEmployee employee)
        {

            await _context.StorageEmployees.AddAsync(employee);
        }
        public void Update(StorageEmployee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public async Task Delete(string id)
        {
            var employee = await GetById(id);
            if (employee != null)
                _context.StorageEmployees.Remove(employee);
        }
    }
}
