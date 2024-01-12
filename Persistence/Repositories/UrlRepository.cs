using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public sealed class UrlRepository : BaseRepository<Url>, IUrlRepository
{
    private readonly UrlDbContext _context;

    public UrlRepository(UrlDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Url?> GetByOriginalUrl(string originalUrl)
    {
        return await _context.Set<Url>().AsNoTracking().FirstOrDefaultAsync(u => u.LongUrl == originalUrl);
    }

    public async Task<Url?> GetByShortPrefix(string base64Prefix)
    {
        return await _context.Set<Url>().AsNoTracking().FirstOrDefaultAsync(u => u.ShortUrl == base64Prefix);
    }
}