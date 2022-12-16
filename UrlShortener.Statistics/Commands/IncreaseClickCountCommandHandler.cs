using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Statistics.Data;
using UrlShortener.Statistics.Entities;

namespace UrlShortener.Statistics.Commands;

public class IncreaseClickCountCommandHandler
    : IRequestHandler<IncreaseClickCountCommand, IncreaseClickCountReply>
{
    private readonly Context _context;

    public IncreaseClickCountCommandHandler(Context context)
    {
        _context = context;
    }

    public async Task<IncreaseClickCountReply> Handle(
        IncreaseClickCountCommand request,
        CancellationToken cancellationToken)
    {
        var entity =
            await _context.ClickCounts.FirstOrDefaultAsync(x => x.ShortId == request.ShortAddress,
                cancellationToken);
        if (entity is null)
        {
            entity = new ClickCount
            {
                Count = 0,
                ShortId = request.ShortAddress
            };
            await _context.AddAsync(entity, cancellationToken);
        }

        entity.Count += 1;
        await _context.SaveChangesAsync(cancellationToken);
        return new IncreaseClickCountReply();
    }
}
