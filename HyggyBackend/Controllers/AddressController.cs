using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
	public class AddressController : Controller
	{
		private readonly IAddressService _addressService;

		public AddressController(IAddressService addressService)
		{
			_addressService = addressService;
		}
		[HttpGet("addresses")]
		public async Task<IActionResult> GetAll()
		{
			var addresses = await _addressService.GetAllAsync();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(addresses);
		}
		[HttpGet]
		[Route("addresses/{id}")]
		public async Task<IActionResult> GetShopById(long id)
		{
			if (!await _addressService.IsAddressExist(id))
				return NotFound();

			var address = await _addressService.GetByIdAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(address);
		}
		[HttpPost]
		[Route("add-new-shop")]
		public async Task<IActionResult> CreateShop([FromBody] AddressDTO addressDto)
		{
			if (!await _addressService.CreateAsync(addressDto))
				return BadRequest(ModelState);

			return Ok(addressDto);
		}

	}
}
