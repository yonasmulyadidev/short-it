using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUrlRepository : IBaseRepository<Url>
{
    public Task<Url?> GetByOriginalUrl(string originalUrl);

    public Task<Url?> GetByShortPrefix(string base64Prefix);
}