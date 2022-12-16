using MediatR;
using Microsoft.EntityFrameworkCore;
using shortid;
using shortid.Configuration;
using UrlShortener.Service.Data;
using UrlShortener.Service.Entities;

namespace UrlShortener.Service.Commands;

public class ShortenAddressCommandHandler
    : IRequestHandler<ShortenAddressCommand, ShortenAddressReply>
{
    private readonly Context _context;

    private static readonly GenerationOptions Options = new(useSpecialCharacters: false,
        useNumbers: false);

    public ShortenAddressCommandHandler(Context context)
    {
        _context = context;
    }

    public async Task<ShortenAddressReply> Handle(
        ShortenAddressCommand request,
        CancellationToken cancellationToken)
    {
        var id = await GenerateUniqueShortId(cancellationToken);
        ShortenedAddress entity = new()
        {
            ShortAddress = id,
            FullAddress = request.FullAddress
        };

        await _context.ShortenedAddresses.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ShortenAddressReply
        {
            ShortAddress = id
        };
    }

    private async Task<string> GenerateUniqueShortId(CancellationToken cancellationToken)
    {
        while (true)
        {
            string id = ShortId.Generate(Options);
            if (!await IsPresentInDb(id, cancellationToken))
            {
                return id;
            }
        }
    }

    private async Task<bool> IsPresentInDb(string shortId, CancellationToken cancellationToken)
    {
        return await _context.ShortenedAddresses.AnyAsync(x => x.ShortAddress == shortId,
            cancellationToken);
    }
}
