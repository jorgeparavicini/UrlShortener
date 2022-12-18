using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.Models;
using UrlShortener.API.Services;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortenerController : ControllerBase
{
    private readonly IShortenerService _service;

    public ShortenerController(IShortenerService service)
    {
        _service = service;
    }

    [HttpGet("{shortAddress}")]
    public async Task<ActionResult<ResolvedAddress>> ResolveShortAddress(
        [FromRoute] string shortAddress)
    {
        var result = await _service.ResolveAddress(shortAddress);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(new ResolvedAddress
        {
            FullAddress = result
        });
    }

    [HttpPost]
    public async Task<ActionResult<ShortAddress>> ShortenAddress(
        [FromBody] Models.ShortenAddressRequest fullAddress)
    {
        var result = await _service.ShortenAddress(fullAddress.FullAddress);
        return Ok(new ShortAddress
        {
            ShortId = result
        });
    }

    [HttpGet("statistics/{shortAddress}")]
    public async Task<ActionResult<Statistic>> GetClickCount([FromRoute] string shortAddress)
    {
        var result = await _service.GetClickCount(shortAddress);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(new Statistic
        {
            Count = result.Value
        });
    }
}
