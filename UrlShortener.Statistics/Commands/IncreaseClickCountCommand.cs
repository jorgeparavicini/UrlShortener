using MediatR;

namespace UrlShortener.Statistics.Commands;

public class IncreaseClickCountCommand : IRequest<IncreaseClickCountReply>
{
    public required string ShortAddress { get; set; }
}
