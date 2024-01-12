using Application.Contracts.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly UrlDbContext _context; 

    protected BaseRepository(UrlDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<T>CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<int> UpdateAsync(T entity)
    {
        _context.Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(T entity)
    {
        _context.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}