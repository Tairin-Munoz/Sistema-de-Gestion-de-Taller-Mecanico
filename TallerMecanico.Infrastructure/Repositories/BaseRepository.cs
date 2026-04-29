using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;

namespace TallerMecanico.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly TallerMecanicoContext _context;
    private readonly DbSet<T> _entities;

    public BaseRepository(TallerMecanicoContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _entities.Remove(entity);
        }
    }
}