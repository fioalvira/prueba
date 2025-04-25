using System.Linq.Expressions;

namespace IDataAccess;
public interface IRepository<T>
{
    T Add(T entity);
    T? Find(Expression<Func<T, bool>> filter);
    T? Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    IList<T> FindAll(params Expression<Func<T, object>>[] includes);
    T? Update(T entity);
    void Delete(Guid id);
}
