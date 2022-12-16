using MediatR;

namespace UrlShortener.Statistics.Queries;

public class GetClickCountQuery : IRequest<GetClickCountReply>
{
    public required string ShortAddress { get; init; }
}
