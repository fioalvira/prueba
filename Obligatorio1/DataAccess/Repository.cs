using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class Repository<T>(ObjectSimulatorDbContext context) : IRepository<T>
    where T : class
{
    private readonly ObjectSimulatorDbContext _context = context;

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T? Find(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach(var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if((typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string)) ||
                (property.PropertyType.IsClass && property.PropertyType != typeof(string)))
            {
                if(_context.Model.FindEntityType(typeof(T))?.FindNavigation(property.Name) != null)
                {
                    query = query.Include(property.Name);
                }
            }
        }

        return query.FirstOrDefault(filter);
    }

    public T? Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach(var include in includes)
        {
            query = query.Include(include);
        }

        return query.FirstOrDefault(filter);
    }

    public IList<T> FindAll(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach(var include in includes)
        {
            query = query.Include(include);
        }

        return query.ToList();
    }

    public T? Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Delete(Guid id)
    {
        var entity = _context.Set<T>().Find(id);
        if(entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
