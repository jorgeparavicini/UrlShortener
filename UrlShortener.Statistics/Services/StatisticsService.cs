using Grpc.Core;
using MediatR;
using UrlShortener.Statistics.Commands;
using UrlShortener.Statistics.Queries;

namespace UrlShortener.Statistics.Services;

public class StatisticsService : Statistics.StatisticsBase
{
    private readonly IMediator _mediator;

    public StatisticsService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<IncreaseClickCountReply> IncreaseClickCount(
        IncreaseClickCountRequest request,
        ServerCallContext context)
    {
        return await _mediator.Send(new IncreaseClickCountCommand
        {
            ShortAddress = request.ShortAddress
        });
    }

    public override async Task<GetClickCountReply> GetClickCount(
        GetClickCountRequest request,
        ServerCallContext context)
    {
        return await _mediator.Send(new GetClickCountQuery
        {
            ShortAddress = request.ShortAddress
        });
    }
}
