using AutoMapper;
using Humanizer;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.DTO.AccountDtos;
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
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.QueryAny, opt => opt.MapFrom(src => src.QueryAny));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers([FromQuery] CustomerQueryPL query)
        {
            try
            {
                IEnumerable<CustomerDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Customer.Id для пошуку!", nameof(CustomerQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<CustomerDTO> { await _serv.GetByIdAsync(query.Id) };
                            }
                        }
                        break;
                    case "Name":
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
                    case "Surname":
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
                    case "Email":
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
                    case "Phone":
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
                    case "OrderId":
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
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано CustomerQuery.StringIds для пошуку!", nameof(CustomerQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Paged":
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
                    case "Query":
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

		//[HttpPost]
		//public async Task<ActionResult<CustomerDTO>> PostCustomer([FromBody] CustomerDTO customer)
		//{
		//    try
		//    {
		//        if (customer == null)
		//        {
		//            throw new ValidationException("CustomerDTO не може бути пустим!", nameof(CustomerDTO));
		//        }
		//        var result = await _serv.CreateAsync(customer);
		//        return Ok(result);
		//    }
		//    catch (ValidationException ex)
		//    {
		//        return StatusCode(500, ex.Message);
		//    }
		//    catch (Exception ex)
		//    {
		//        return StatusCode(500, ex.Message);
		//    }
		//}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserForRegistrationDto registrationDto)
		{
			try
			{
				if (registrationDto is null)
					return BadRequest();

				var response = await _serv.RegisterAsync(registrationDto);
				if (!response.IsSuccessfullRegistration)
					return BadRequest(response.Errors);

				return Ok(new { message = "Ваш обліковий запис створено. Ми відправили підтвердження на вашу пошту. Будь ласка, активуйте свій обліковий запис, натиснувши на кнопку у листі. Якщо ви не отримали листа, будь ласка, перевірте папку зі спамом або спробуйте увійти у свій обліковий запис ще раз, для того щоб ми надіслали вам повторний лист для активації." });
			}
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createOrFindGuest")]
        public async Task<IActionResult> CreateOrFindGuest([FromBody] CustomerDTO GuestDTO)
        {
            try
            {
                if (GuestDTO is null)
                    return BadRequest();

                var response = await _serv.CreateOrFindGuestCustomerAsync(GuestDTO);
               
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("authenticate")]
		public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto authenticationDto)
		{
			try
			{
				var response = await _serv.AuthenticateAsync(authenticationDto);
				if (!response.IsAuthSuccessfull)
					return StatusCode(500, response.Error);

				return Ok(response);
			}
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
		[HttpGet("emailconfirmation")]
		public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
		{
			try
			{
				var result = await _serv.EmailConfirmation(email, token);

				return Ok(result);
			}
			catch (ValidationException ex)
			{
				return StatusCode(500, ex.Message);
			}
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(string id)
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class CustomerQueryPL
    {
        public string SearchParameter { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? OrderId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
    }
}
