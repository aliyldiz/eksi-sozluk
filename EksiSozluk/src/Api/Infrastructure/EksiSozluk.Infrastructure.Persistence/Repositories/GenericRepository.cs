using System.Linq.Expressions;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _context;
    protected DbSet<TEntity> entity => _context.Set<TEntity>();
    
    public GenericRepository(DbContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await this.entity.AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public virtual int Add(TEntity entity)
    {
        this.entity.Add(entity);
        return _context.SaveChanges();
    }

    public virtual int Add(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;
        
        entity.AddRange(entity);
        return _context.SaveChanges();
    }

    public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;
        
        await entity.AddRangeAsync(entities);
        return await _context.SaveChangesAsync();
    }

    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }

    public virtual int Update(TEntity entity)
    {
        this.entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return _context.SaveChanges();
    }

    public virtual Task<int> DeleteAsync(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }
        this.entity.Remove(entity);
        return _context.SaveChangesAsync();
    }

    public virtual int Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            this.entity.Attach(entity);
        
        this.entity.Remove(entity);
        return _context.SaveChanges();
    }

    public virtual Task<int> DeleteAsync(Guid id)
    {
        var entity = this.entity.Find(id);
        return DeleteAsync(entity);
    }

    public virtual int Delete(Guid id)
    {
        var entity = this.entity.Find(id);
        return Delete(entity);
    }

    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(entity.Where(predicate));
        return _context.SaveChanges() > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(entity.Where(predicate));
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual Task<int> AddOrUpdateAsync(TEntity entity)
    {
        if (!this.entity.Local.Any(e => EqualityComparer<Guid>.Default.Equals(e.Id, entity.Id)))
            _context.Update(entity);
        return _context.SaveChangesAsync();
    }

    public virtual int AddOrUpdate(TEntity entity)
    {
        if (!this.entity.Local.Any(e => EqualityComparer<Guid>.Default.Equals(e.Id, entity.Id)))
            _context.Update(entity);
        return _context.SaveChanges();
    }

    public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

    public virtual async Task<List<TEntity>> GetAllAsync(bool noTracking = true)
    {
        if (noTracking)
            return await entity.AsNoTracking().ToListAsync();
        
        return await entity.ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
            query = query.Where(predicate);

        foreach (Expression<Func<TEntity, object>> include in includes)
            query = query.Include(include);
        
        if (orderBy != null)
            query = orderBy(query);
        
        if (noTracking)
            query = query.AsNoTracking();
        
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        TEntity found = await entity.FindAsync(id);
        
        if (found == null)
            return null;
        
        if (noTracking)
            _context.Entry(found).State = EntityState.Detached;
        
        foreach (Expression<Func<TEntity, object>> include in includes)
            _context.Entry(found).Reference(include).Load();
        
        return found;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;
        
        if (predicate != null)
            query = query.Where(predicate);
        
        query = ApplyIncludes(query, includes);
        
        if (noTracking)
            query = query.AsNoTracking();
        
        return await query.SingleOrDefaultAsync() ?? throw new InvalidOperationException();
    }

    public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        return Get(predicate, noTracking, includes).FirstOrDefaultAsync();
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);
        
        query = ApplyIncludes(query, includes);
        
        if (noTracking)
            query = query.AsNoTracking();
        
        return query;
    }

    public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
    {
        if (ids != null && !ids.Any())
            return Task.CompletedTask;
        
        _context.RemoveRange(entity.Where(e => ids.Contains(e.Id)));
        return _context.SaveChangesAsync();
    }

    public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(entity.Where(predicate));
        return _context.SaveChangesAsync();
    }

    public virtual Task BulkDelete(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;
        
        entity.RemoveRange(entities);
        return _context.SaveChangesAsync();
    }

    public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;
        
        foreach (var entityItem in entities)
        {
            entity.Update(entityItem);
        }
        
        return _context.SaveChangesAsync();
    }

    public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            await Task.CompletedTask;
        
        await entity.AddRangeAsync(entities);
        
        await _context.SaveChangesAsync();
    }
    
    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
    
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
    
    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
        {
            foreach (var includeItem in includes)
            {
                query = query.Include(includeItem);
            }
        }
        return query;
    }
}