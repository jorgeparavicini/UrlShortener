using Microsoft.EntityFrameworkCore;
using UrlShortener.Statistics.Entities;

namespace UrlShortener.Statistics.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<ClickCount> ClickCounts => Set<ClickCount>();
}
