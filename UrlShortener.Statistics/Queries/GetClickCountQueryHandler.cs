using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Statistics.Data;

namespace UrlShortener.Statistics.Queries;

public class GetClickCountQueryHandler
    : IRequestHandler<GetClickCountQuery, GetClickCountReply>
{
    private readonly Context _context;

    public GetClickCountQueryHandler(Context context)
    {
        _context = context;
    }

    public async Task<GetClickCountReply> Handle(
        GetClickCountQuery request,
        CancellationToken cancellationToken)
    {
        var entity =
            await _context.ClickCounts.FirstOrDefaultAsync(x => x.ShortId == request.ShortAddress,
                cancellationToken);
        var count = entity?.Count ?? 0;
        return new GetClickCountReply
        {
            ClickCount = count
        };
    }
}
