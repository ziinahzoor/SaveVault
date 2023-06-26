using Microsoft.AspNetCore.Mvc;

namespace SaveVault.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
	private readonly ILogger<UploadController> _logger;
	private readonly IUploadService _uploadService;

	public UploadController(ILogger<UploadController> logger, IUploadService uploadService)
	{
		_logger = logger;
		_uploadService = uploadService;
	}

	[HttpPost("PC")]
	public IActionResult UploadPC([FromBody] PCSave save)
	{
		try
		{
			_uploadService.Upload(save);
			return Ok();
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}

	[HttpPost("Mobile")]
	public IActionResult UploadMobile([FromBody] MobileSave save)
	{
		try
		{
			_uploadService.Upload(save);
			return Ok();
		}
		catch (Exception exception)
		{
			return BadRequest(exception);
		}
	}
}