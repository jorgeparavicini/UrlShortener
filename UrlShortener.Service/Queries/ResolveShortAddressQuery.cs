using MediatR;

namespace UrlShortener.Service.Queries;

public class ResolveShortAddressQuery : IRequest<ResolveShortAddressReply>
{
    public required string ShortAddress { get; init; }
}
