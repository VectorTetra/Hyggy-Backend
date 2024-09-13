using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
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
	}
}
