﻿using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarePriceHistoryController : ControllerBase
    {
        private readonly IWarePriceHistoryService _serv;

        public WarePriceHistoryController(IWarePriceHistoryService serv)
        {
            _serv = serv;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WarePriceHistoryQueryPL, WarePriceHistoryQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
             .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
             .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
             .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
             .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarePriceHistoryDTO>>> GetWarePriceHistories([FromQuery] WarePriceHistoryQueryPL query)
        {
            try
            {
                IEnumerable<WarePriceHistoryDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "GetById":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано WarePriceHistory.Id для пошуку!", nameof(WarePriceHistoryQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WarePriceHistoryDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "GetByWareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано WarePriceHistory.WareId для пошуку!", nameof(WarePriceHistoryQueryPL.WareId));
                            }
                            else
                            {
                                collection = await _serv.GetByWareId(query.WareId.Value);
                            }
                        }
                        break;
                    case "GetByPriceRange":
                        {
                            if (query.MinPrice == null)
                            {
                                throw new ValidationException("Не вказано WarePriceHistory.MinPrice для пошуку!", nameof(WarePriceHistoryQueryPL.MinPrice));
                            }
                            else if (query.MaxPrice == null)
                            {
                                throw new ValidationException("Не вказано WarePriceHistory.MaxPrice для пошуку!", nameof(WarePriceHistoryQueryPL.MaxPrice));
                            }
                            else
                            {
                                collection = await _serv.GetByPriceRange(query.MinPrice.Value, query.MaxPrice.Value);
                            }
                        }
                        break;
                    case "GetByDateRange":
                        {
                            if (query.StartDate == null)
                            {
                                throw new ValidationException("Не вказано WarePriceHistory.StartDate для пошуку!", nameof(WarePriceHistoryQueryPL.StartDate));
                            }
                            else if (query.EndDate == null)
                            {
                                throw new ValidationException("Не вказано WarePriceHistory.EndDate для пошуку!", nameof(WarePriceHistoryQueryPL.EndDate));
                            }
                            else
                            {
                                collection = await _serv.GetByDateRange(query.StartDate.Value, query.EndDate.Value);
                            }
                        }
                        break;
                    case "GetByQuery":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<WarePriceHistoryQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр WarePriceHistoryQuery.SearchParameter!", nameof(WarePriceHistoryQueryPL.SearchParameter));
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
        public async Task<ActionResult<WarePriceHistoryDTO>> CreateWarePriceHistory([FromBody] WarePriceHistoryDTO warePriceHistory)
        {
            try
            {
                if (warePriceHistory == null)
                {
                    throw new ValidationException("Не вказано WarePriceHistory для створення!", nameof(WarePriceHistoryDTO));
                }
                var result = await _serv.Create(warePriceHistory);
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
        public async Task<ActionResult<WarePriceHistoryDTO>> UpdateWarePriceHistory([FromBody] WarePriceHistoryDTO warePriceHistory)
        {
            try
            {
                if (warePriceHistory == null)
                {
                    throw new ValidationException("Не вказано WarePriceHistory для оновлення!", nameof(WarePriceHistoryDTO));
                }
                var result = await _serv.Update(warePriceHistory);
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
        public async Task<ActionResult<WarePriceHistoryDTO>> DeleteWarePriceHistory(long id)
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

    public class WarePriceHistoryQueryPL
    {
        public string? SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? WareId { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}