using Microsoft.AspNetCore.Mvc;
using SaveVault.Helpers;
using SaveVault.Models;
using SaveVault.Services;

namespace SaveVault.Controllers;

[ApiController]
[Route("[controller]")]
public class DownloadController : ControllerBase
{
	private readonly ILogger<DownloadController> _logger;
	private readonly IUserService _userService;
	private readonly IGameService _gameService;
	private readonly IDownloadService _downloadService;
	private readonly IConversionService _conversionService;

	public DownloadController(ILogger<DownloadController> logger,
							  IUserService userService,
							  IGameService gameService,
							  IDownloadService downloadService,
							  IConversionService conversionService)
	{
		_logger = logger;
		_userService = userService;
		_gameService = gameService;
		_downloadService = downloadService;
		_conversionService = conversionService;
	}

	[HttpPost("Latest")]
	public IActionResult GetLatest([FromBody] QueryDto query)
	{
		try
		{
			User user = _userService.GetById(query.UserId);
			Game game = _gameService.GetById(query.GameId);

			UniversalSave save = (UniversalSave)_downloadService.GetLatest(game, user);

			var platformSave = _conversionService.Convert<UniversalSave, PlatformSave>(save, query.TargetPlatform);
			var saveString = SaveIOHelper.Deserialize(platformSave);

			byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(saveString);
			string contentType = "application/octet-stream";
			string fileName = $"{platformSave.Id}.psav";

			return File(fileBytes, contentType, fileName);
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}


	// [HttpGet("GetAll")]
	// public IActionResult GetAll(Platform targetPlatform, Guid gameId, Guid userId)
	// {
	// 	try
	// 	{
	// 		var user = _userService.GetById(userId);
	// 		var game = _gameService.GetById(gameId);
	// 		var saves = _downloadService.GetAllSaves(game, user);
	// 		var convertedSaves = _conversionService.ConvertAll<ISave, ISave>(saves);

	// 		return Ok(convertedSaves);
	// 	}
	// 	catch (Exception exception)
	// 	{
	// 		return BadRequest(exception);
	// 	}
	// }


	// [HttpGet("GetById")]
	// public IActionResult GetById(Platform targetPlatform, Guid saveId, Guid userId)
	// {
	// 	try
	// 	{
	// 		var user = _userService.GetById(userId);
	// 		var save = _downloadService.GetById(saveId, user);

	// 		PCSave targetSave;
	// 		var convertedSave = _conversionService.Convert(save, out targetSave);

	// 		return Ok(convertedSave);
	// 	}
	// 	catch (Exception exception)
	// 	{
	// 		return BadRequest(exception);
	// 	}
	// }

	// [HttpGet("GetLatest")]
	// public IActionResult GetLatest(Platform targetPlatform, Guid gameId, Guid userId)
	// {
	// 	try
	// 	{
	// 		var user = _userService.GetById(userId);
	// 		var game = _gameService.GetById(gameId);
	// 		var save = _downloadService.GetLatest(game, user);
	// 		var convertedSave = _conversionService.Convert<ISave, ISave>(save);

	// 		return Ok(convertedSave);
	// 	}
	// 	catch (Exception exception)
	// 	{
	// 		return BadRequest(exception);
	// 	}
	// }
}