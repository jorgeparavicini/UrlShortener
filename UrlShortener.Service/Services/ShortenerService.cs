using Grpc.Core;
using MediatR;
using UrlShortener.Service.Commands;
using UrlShortener.Service.Queries;

namespace UrlShortener.Service.Services;

public class ShortenerService : Shortener.ShortenerBase
{
    private readonly IMediator _mediator;

    public ShortenerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<ShortenAddressReply> ShortenAddress(
        ShortenAddressRequest request,
        ServerCallContext context)
    {
        return await _mediator.Send(new ShortenAddressCommand
        {
            FullAddress = request.FullAddress
        });
    }

    public override async Task<ResolveShortAddressReply> ResolveShortAddress(
        ResolveShortAddressRequest request,
        ServerCallContext context)
    {
        return await _mediator.Send(new ResolveShortAddressQuery
        {
            ShortAddress = request.ShortAddress
        });
    }
}
