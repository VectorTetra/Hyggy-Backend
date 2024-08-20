using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
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
			if(! await _shopService.Create(shopDto))
				return BadRequest(ModelState);

			return Ok(shopDto);
		}
	}
}
