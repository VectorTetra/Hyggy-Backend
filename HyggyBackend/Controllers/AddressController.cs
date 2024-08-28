using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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
		public async Task<IActionResult> GetAddressById(long id)
		{
			if (!await _addressService.IsAddressExist(id))
				return NotFound();

			var address = await _addressService.GetByIdAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(address);
		}
		[HttpPost]
		[Route("add-new-address")]
		public async Task<IActionResult> CreateAddress([FromBody] AddressDTO addressDto)
		{
			//if (!await _addressService.CreateAsync(addressDto))
			//{
			//	ModelState.AddModelError("", "Щось пішло не так при створенні адреси");
			//	return StatusCode(500, ModelState);
			//}
			await _addressService.CreateAsync(addressDto);
			return Ok(addressDto);
		}
		[HttpPut]
		[Route("update-address")]
		public async Task<IActionResult> UpdateAddress([FromBody] AddressDTO address)
		{
			if(address == null)
				return BadRequest(ModelState);

			if(!await _addressService.IsAddressExist(address.Id))
				return NotFound();

			if(!ModelState.IsValid) 
				return BadRequest(ModelState);

			//if(!await _addressService.Update(address))
			//{
			//	ModelState.AddModelError("", "Щось пішло не так при оновленні адреси");
			//	return StatusCode(500, ModelState);
			//}
			_addressService.Update(address);
			return Ok("Адреса вдало оновлена");
		}
		[HttpDelete("{addressId}")]
		public async Task<IActionResult> DeleteShop(int addressId)
		{
			if (!await _addressService.IsAddressExist(addressId))
				return NotFound();

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			//if(!await _addressService.DeleteAsync(addressId))
			//{
			//	ModelState.AddModelError("", "Щось пішло не так при видаленні адреси");
			//	return StatusCode(500, ModelState);
			//}
			await _addressService.DeleteAsync(addressId);
			return Ok("Адресу видалено");
		}
	}
}
