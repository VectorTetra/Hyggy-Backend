using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/shop")]
	public class ShopController : Controller
	{
		private readonly IShopService _shopService;
		public ShopController(IShopService shopService)
		{
			_shopService = shopService;
		}
		
		[HttpGet("shops")]
		//[Authorize]
		public async Task<IActionResult> GetAll()
		{
			var shops = await _shopService.GetAll();

			if(!ModelState.IsValid) 
				return BadRequest(ModelState);

			return Ok(shops);
		}
		[HttpGet]
		[Route("shops/{id}")]
		public async Task<IActionResult> GetShopById(long id)
		{
			if(! await _shopService.IsShopExist(id))
				return NotFound();

			var shop = await _shopService.GetById(id);

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(shop);
		}
		[HttpPost]
		[Route("add-new-shop")]
		public async Task<IActionResult> CreateShop([FromBody] ShopDTO shopDto)
		{
			try
			{
				if (await _shopService.IsShopExistByAddress(shopDto.AddressId))
					return Ok("За такою адресою вже існує зареєстрований магазин");

				await _shopService.Create(shopDto);
				return Ok("Магазин успішно доданий");
			}
			catch(ValidationException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut]
		[Route("update-shop")]
		public async Task<IActionResult> UpdateShop([FromBody] ShopDTO shopDto)
		{
			try
			{
				if (shopDto == null)
					return BadRequest();

				if (!await _shopService.IsShopExist(shopDto.Id))
					return NotFound();

				_shopService.Update(shopDto);
				return Ok("Магазин вдало оновлен");
			}
			catch (ValidationException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("{shopId}")]
		public async Task<IActionResult> DeleteShop(int shopId)
		{
			try
			{
				if (!await _shopService.IsShopExist(shopId))
					return NotFound();

				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				await _shopService.Delete(shopId);
				return Ok("Магазин видалено");
			}
			catch (ValidationException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
