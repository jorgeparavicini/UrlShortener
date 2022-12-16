using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Service.Data;

namespace UrlShortener.Service.Queries;

public class ResolveShortAddressQueryHandler
    : IRequestHandler<ResolveShortAddressQuery, ResolveShortAddressReply>
{
    private readonly Context _context;

    public ResolveShortAddressQueryHandler(Context context)
    {
        _context = context;
    }

    public async Task<ResolveShortAddressReply> Handle(
        ResolveShortAddressQuery request,
        CancellationToken cancellationToken)
    {
        var fullAddress = (await _context.ShortenedAddresses
            .FirstOrDefaultAsync(x => x.ShortAddress == request.ShortAddress,
                cancellationToken: cancellationToken))?.FullAddress ?? string.Empty;
        return new ResolveShortAddressReply
        {
            FullAddress = fullAddress
        };
    }
}
