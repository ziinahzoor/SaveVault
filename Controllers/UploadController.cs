using Microsoft.AspNetCore.Mvc;

namespace SaveVault.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
	private readonly ILogger<UploadController> _logger;

	public UploadController(ILogger<UploadController> logger)
	{
		_logger = logger;
	}

	[HttpPost("UploadPC")]
	public IActionResult Post([FromBody] PCSave save)
	{
		_logger.LogInformation("Post realizado");
		return Ok();
	}
}