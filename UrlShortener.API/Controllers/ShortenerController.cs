using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.Services;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortenerController : ControllerBase
{
    private readonly ILogger<ShortenerController> _logger;
    private readonly IShortenerService _service;

    public ShortenerController(ILogger<ShortenerController> logger, IShortenerService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("{shortAddress}")]
    public async Task<ActionResult<string>> ResolveShortAddress([FromRoute] string shortAddress)
    {
        var result = await _service.ResolveAddress(shortAddress);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<string>> ShortenAddress([FromBody] string fullAddress)
    {
        return Ok(await _service.ShortenAddress(fullAddress));
    }

    [HttpGet("statistics/{shortAddress}")]
    public async Task<ActionResult<int>> GetClickCount([FromRoute] string shortAddress)
    {
        var result = await _service.GetClickCount(shortAddress);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
