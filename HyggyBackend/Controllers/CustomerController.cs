using AutoMapper;
using Humanizer;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _serv;
        IWebHostEnvironment _appEnvironment;

        public CustomerController(ICustomerService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<CustomerQueryPL, CustomerQueryBLL>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers([FromQuery] CustomerQueryPL query)
        {
            try
            {
                IEnumerable<CustomerDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "GetById":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Customer.Id для пошуку!", nameof(CustomerQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<CustomerDTO> { await _serv.GetByIdAsync(query.Id.Value) };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (query.Name == null)
                            {
                                throw new ValidationException("Не вказано Customer.Name для пошуку!", nameof(CustomerQueryPL.Name));
                            }
                            else
                            {
                                collection = await _serv.GetByNameSubstring(query.Name);
                            }
                        }
                        break;
                    case "GetBySurname":
                        {
                            if (query.Surname == null)
                            {
                                throw new ValidationException("Не вказано Customer.Surname для пошуку!", nameof(CustomerQueryPL.Surname));
                            }
                            else
                            {
                                collection = await _serv.GetBySurnameSubstring(query.Surname);
                            }
                        }
                        break;
                    case "GetByEmail":
                        {
                            if (query.Email == null)
                            {
                                throw new ValidationException("Не вказано Customer.Email для пошуку!", nameof(CustomerQueryPL.Email));
                            }
                            else
                            {
                                collection = await _serv.GetByEmailSubstring(query.Email);
                            }
                        }
                        break;
                    case "GetByPhone":
                        {
                            if (query.Phone == null)
                            {
                                throw new ValidationException("Не вказано Customer.Phone для пошуку!", nameof(CustomerQueryPL.Phone));
                            }
                            else
                            {
                                collection = await _serv.GetByPhoneSubstring(query.Phone);
                            }
                        }
                        break;
                    case "GetByOrderId":
                        {
                            if (query.OrderId == null)
                            {
                                throw new ValidationException("Не вказано Order.Id для пошуку!", nameof(CustomerQueryPL.OrderId));
                            }
                            else
                            {
                                collection = await _serv.GetByOrderId(query.OrderId.Value);
                            }
                        }
                        break;
                    case "GetPaged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано CustomerQuery.PageNumber для пошуку!", nameof(CustomerQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано CustomerQuery.PageSize для пошуку!", nameof(CustomerQueryPL.PageSize));
                            }
                            collection = await _serv.GetPagedCustomers(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "GetByQuery":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<CustomerQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр CustomerQuery.SearchParameter!", nameof(CustomerQueryPL.SearchParameter));
                        }

                }
                if (collection.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return collection?.ToList();
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new ValidationException("CustomerDTO не може бути пустим!", nameof(CustomerDTO));
                }
                var result = await _serv.CreateAsync(customer);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CustomerDTO>> PutCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new ValidationException("CustomerDTO не може бути пустим!", nameof(CustomerDTO));
                }
                var dto =  await _serv.Update(customer);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(long id)
        {
            try
            {
                var customer = await _serv.GetByIdAsync(id);
                if (customer == null)
                {
                    throw new ValidationException("Customer з таким Id не знайдено!", nameof(id));
                }
                await _serv.DeleteAsync(id);
                return customer;
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class CustomerQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? OrderId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
