using MediatR;

namespace UrlShortener.Service.Commands;

public class ShortenAddressCommand : IRequest<ShortenAddressReply>
{
    public required string FullAddress { get; init; }
}
