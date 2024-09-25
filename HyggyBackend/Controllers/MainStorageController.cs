using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/mainstorage")]
	public class MainStorageController : ControllerBase
	{
		private readonly IMainStorageService _mainStorageService;

		public MainStorageController(IMainStorageService mainStorageService)
		{
			_mainStorageService = mainStorageService;
		}
		[HttpPost("addstorage")]
		public async Task<IActionResult> Create([FromBody] MainStorageDto storageDto)
		{
			try
			{
				await _mainStorageService.Create(storageDto);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("updatestorage")]
		public async Task<IActionResult> UpdateStorage([FromBody] MainStorageDto storageDto)
		{
			try
			{
				if (storageDto is null)
					return BadRequest();

				if (!await _mainStorageService.IsStorageExist(storageDto.Id))
					return NotFound();

				_mainStorageService.Update(storageDto);
				return Ok("Склад оновлено");
			}
			catch(ValidationException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("deletestorage/{storageId}")]
		public async Task<IActionResult> DeleteStorage(long storageId)
		{
			try
			{
				if (!await _mainStorageService.IsStorageExist(storageId))
					return NotFound();

				await _mainStorageService.Delete(storageId);
				return Ok("Склад видалено");
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
