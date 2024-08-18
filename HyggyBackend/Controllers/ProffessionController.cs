using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.Controllers
{
    [Route("api/Proffession")]
    [ApiController]
    public class ProffessionController : ControllerBase
    {
        private readonly IProffessionService _serv;

        public ProffessionController(IProffessionService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProffessionDTO>>> GetProffession([FromQuery] ProffessionQuery proffessionQuery)
        {
            try
            {
                IEnumerable<ProffessionDTO?> collection = null;
                switch (proffessionQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (proffessionQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(ProffessionQuery.Id));
                            }
                            else
                            {
                                collection = new List<ProffessionDTO> { await _serv.GetById((long)proffessionQuery.Id) };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (proffessionQuery.Name == null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(proffessionQuery.Name));
                            }
                            collection = await _serv.GetByName(proffessionQuery.Name);
                        }
                        break;
                    case "GetByEmployeeName":
                        {
                            if (proffessionQuery.EmployeeName == null)
                            {
                                throw new ValidationException("Не вказано EmployeeName для пошуку!", nameof(proffessionQuery.EmployeeName));
                            }
                            collection = await _serv.GetByEmployeeName(proffessionQuery.EmployeeName);
                        }
                        break;
                    case "GetByEmployeeSurname":
                        {
                            if (proffessionQuery.EmployeeSurname == null)
                            {
                                throw new ValidationException("Не вказано EmployeeSurname для пошуку!", nameof(proffessionQuery.EmployeeSurname));
                            }
                            collection = await _serv.GetByEmployeeSurname(proffessionQuery.EmployeeSurname);
                        }
                        break;
                    default:
                        {
                            collection = new List<ProffessionDTO>();
                        }
                        break;
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
        public async Task<ActionResult<ProffessionDTO>> CreateProffession(ProffessionDTO proffession)
        {
            try
            {
                if (proffession == null)
                {
                    throw new ValidationException("Не вказано proffession для створення!", nameof(ProffessionDTO));
                }
                var result = await _serv.Create(proffession);
                return result;
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
        public async Task<ActionResult<ProffessionDTO>> UpdateProffession(ProffessionDTO proffession)
        {
            try
            {
                if (proffession == null)
                {
                    throw new ValidationException("Не вказано proffession для оновлення!", nameof(ProffessionDTO));
                }
                var result = await _serv.Update(proffession);
                return result;
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

        [HttpDelete]
        public async Task<ActionResult<ProffessionDTO>> DeleteProffession(long id)
        {
            try
            {
                var result = await _serv.Delete(id);
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
    }
    public class ProffessionQuery
    {
        public string? SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeSurname { get; set; }
    }
}
