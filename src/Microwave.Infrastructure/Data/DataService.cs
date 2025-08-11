using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Interfaces;

namespace Microwave.Infrastructure.Data;

public class DataService<TEntity> : IDataService<TEntity> where TEntity : class
{
    private readonly MicroWaveDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public DataService(MicroWaveDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity?> GetByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}