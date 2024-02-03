using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class UrlDbContext : DbContext
{
    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
    {
    }
    
    public DbSet<Url> Urls { get; set; }
}