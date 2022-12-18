using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlShortener.API.Controllers;
using UrlShortener.API.Services;

namespace UrlShortener.Test.Api.Controllers;

public class ShortenerControllerTest
{
    [Fact]
    public async Task ResolveShortAddress_ReturnsNotFound_WhenAddressCouldNotBeResolved()
    {
        // Arrange
        var service = new Mock<IShortenerService>();
        service.Setup(s => s.ResolveAddress("shortId")).ReturnsAsync(value: null);
        var controller = new ShortenerController(service.Object);

        // Act
        var result = await controller.ResolveShortAddress("shortId");

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
        result.Result.As<NotFoundResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
