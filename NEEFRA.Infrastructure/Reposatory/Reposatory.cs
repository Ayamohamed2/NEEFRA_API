using System.Linq.Expressions;
using System.Linq;

using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;
using NEEFRA.Domain.IReposatory;

namespace Villa_API_Project.DataAccess.Reposatory
{
    public class Reposatory<T> : IReposatory<T> where T : class
    {

        private readonly IMongoCollection<T> _collection;

        public Reposatory(MongoDbContext context)
        {
            // Collection name = اسم الكلاس + s
            var collectionName = typeof(T).Name + "s";
            Console.WriteLine(collectionName);
            _collection = context.Database.GetCollection<T>(collectionName);
        }

        public async Task CreateAsync(T model)
        {
            await _collection.InsertOneAsync(model);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            int pageSize = 0,
            int pageNumber = 1)
        {
            var query = filter == null
                ? _collection.Find(_ => true)
                : _collection.Find(filter);

            if (pageSize > 0)
            {
                if (pageSize > 100) pageSize = 100;

                query = query
                    .Skip(pageSize * (pageNumber - 1))
                    .Limit(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            Console.WriteLine(_collection);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Expression<Func<T, bool>> filter, T entity)
        {
            await _collection.ReplaceOneAsync(filter, entity);
        }


    }
}

