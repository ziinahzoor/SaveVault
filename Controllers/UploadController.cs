using Microsoft.AspNetCore.Mvc;
using SaveVault.Models;
using SaveVault.Services;

namespace SaveVault.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
	private readonly ILogger<UploadController> _logger;
	private readonly IUploadService _uploadService;
	private readonly IConversionService _conversionService;

	public UploadController(ILogger<UploadController> logger,
							IUploadService uploadService,
							IConversionService conversionService)
	{
		_logger = logger;
		_uploadService = uploadService;
		_conversionService = conversionService;
	}

	[HttpPost]
	public IActionResult Upload([FromForm] List<IFormFile> files)
	{
		try
		{
			foreach (IFormFile file in files)
			{
				PlatformSave save = _conversionService.ConvertFromFile<PlatformSave>(file);
				UniversalSave universalSave = _conversionService.Convert(save);
				_uploadService.Upload(universalSave);
			}

			return Ok();
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}
}