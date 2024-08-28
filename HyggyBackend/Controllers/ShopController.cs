using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ShopController : Controller
	{
		private readonly IShopService _shopService;
		public ShopController(IShopService shopService)
		{
			_shopService = shopService;
		}
		
		[HttpGet("shops")]
		[Authorize]
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
			if (await _shopService.IsShopExistByAddress(shopDto.AddressId))
				return Ok("За такою адресою вже існує зареєстрований магазин");

			//if (! await _shopService.Create(shopDto))
			//{
			//	ModelState.AddModelError("", "Щось пішло не так при створенні магазину");
			//	return StatusCode(500, ModelState);
			//}

			await _shopService.Create(shopDto);
			return Ok("Магазин успішно доданий");
		}
		[HttpPut]
		[Route("update-shop")]
		public async Task<IActionResult> UpdateShop([FromBody] ShopDTO shopDto)
		{
			if (shopDto == null)
				return BadRequest();

			if(!await _shopService.IsShopExist(shopDto.Id))
				return NotFound();

			//if(! await _shopService.Update(shopDto))
			//{
			//	ModelState.AddModelError("", "Щось пішло не так при оновленні магазину");
			//	return StatusCode(500, ModelState);
			//}

			_shopService.Update(shopDto);
			return Ok("Магазин вдало оновлен");
		}
		[HttpDelete("{shopId}")]
		public async Task<IActionResult> DeleteShop(int shopId)
		{
			if (!await _shopService.IsShopExist(shopId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			//if (!await _shopService.Delete(shopId))
			//{
			//	ModelState.AddModelError("", "Щось пішло не так при видаленні магазину");
			//	return StatusCode(500, ModelState);
			//}

			await _shopService.Delete(shopId);
			return Ok("Магазин видалено");
		}
	}
}
