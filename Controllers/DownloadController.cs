using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
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
	public async Task<IActionResult> GetLatest([FromBody] QueryDto query)
	{
		try
		{
			User user = await _userService.GetById(query.UserId);
			Game game = await _gameService.GetById(query.GameId);

			UniversalSave save = (UniversalSave)await _downloadService.DownloadLatest(game, user);

			if (save == null)
			{
				return NotFound();
			}

			PlatformSave platformSave = _conversionService.Convert(save, query.TargetPlatform);
			SVFile file = _downloadService.CreatePlatformFile(platformSave.Id.ToString(), platformSave.ToFileString());
			return File(file.Content, file.Type, file.Name);
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}

	[HttpPost("All")]
	public async Task<IActionResult> GetAll([FromBody] QueryDto query)
	{
		try
		{
			User user = await _userService.GetById(query.UserId);
			Game game = await _gameService.GetById(query.GameId);

			IEnumerable<UniversalSave> saves = (IEnumerable<UniversalSave>)await _downloadService.DownloadAllSaves(game, user);

			if (!saves.Any())
			{
				return NotFound();
			}

			using var outputStream = new MemoryStream();
			using var archive = new ZipArchive(outputStream, ZipArchiveMode.Create, true);

			foreach (var save in saves)
			{
				PlatformSave platformSave = _conversionService.Convert(save, query.TargetPlatform);
				SVFile file = _downloadService.CreatePlatformFile(platformSave.Id.ToString(), platformSave.ToFileString());

				var file1 = archive.CreateEntry(file.Name);
				using var binaryWriter = new BinaryWriter(file1.Open());
				binaryWriter.Write(file.Content);
			}

			return File(outputStream.ToArray(), "application/zip", "saves.zip");
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}


	[HttpGet("ById")]
	public async Task<IActionResult> GetById([FromBody] QueryDto query)
	{
		try
		{
			UniversalSave save = (UniversalSave)await _downloadService.DownloadById(query.SaveId);

			if (save == null)
			{
				return NotFound();
			}

			PlatformSave platformSave = _conversionService.Convert(save, query.TargetPlatform);
			SVFile file = _downloadService.CreatePlatformFile(platformSave.Id.ToString(), platformSave.ToFileString());
			return File(file.Content, file.Type, file.Name);
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}
}