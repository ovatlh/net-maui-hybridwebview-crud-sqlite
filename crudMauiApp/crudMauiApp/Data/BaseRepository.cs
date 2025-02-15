using SQLite;
using System.Linq.Expressions;

namespace crudMauiApp.Data
{
    public enum OrderType
    {
        ASC,
        DESC
    }

    public interface IRepository<T> where T : class, new()
    {
        AsyncTableQuery<T> AsQueryable();
        Task<int> CreateAsync(T entity);
        Task<List<T>> ReadAsync<TValue>(Expression<Func<T, bool>> filter, Expression<Func<T, TValue>> orderBy, OrderType order);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        public AsyncTableQuery<T> AsQueryable()
        {
            return App.GlobalApp.DBContext.dbConnection.Table<T>();
        }

        public async Task<int> CreateAsync(T entity)
        {
            return await App.GlobalApp.DBContext.dbConnection.InsertAsync(entity);
        }

        public async Task<List<T>> ReadAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, OrderType order = OrderType.DESC)
        {
            return await ReadAsync<object>(filter, orderBy, order);
        }

        public async Task<List<T>> ReadAsync<TValue>(Expression<Func<T, bool>>? filter = null, Expression<Func<T, TValue>>? orderBy = null, OrderType order = OrderType.DESC)
        {
            var query = AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            orderBy ??= (Expression<Func<T, TValue>>)(object)((Expression<Func<T, object>>)(x => (object)x.DateTimeCreated));

            switch (order)
            {
                case OrderType.ASC:
                    query = query.OrderBy(orderBy);
                    break;
                case OrderType.DESC:
                    query = query.OrderByDescending(orderBy);
                    break;
                default:
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await App.GlobalApp.DBContext.dbConnection.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await App.GlobalApp.DBContext.dbConnection.DeleteAsync(entity);
        }
    }
}