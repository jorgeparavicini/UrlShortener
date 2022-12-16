using Microsoft.EntityFrameworkCore;
using UrlShortener.Service.Entities;

namespace UrlShortener.Service.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<ShortenedAddress> ShortenedAddresses => Set<ShortenedAddress>();
}
