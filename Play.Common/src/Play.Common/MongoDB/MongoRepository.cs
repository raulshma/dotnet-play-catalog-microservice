using System.Linq.Expressions;
using MongoDB.Driver;

namespace Play.Common.MongoDB;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> DbCollection;
    private readonly FilterDefinitionBuilder<T> FilterBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        DbCollection = mongoDatabase.GetCollection<T>(collectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await DbCollection.Find(FilterBuilder.Empty).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await DbCollection.Find(filter).ToListAsync();
    }

    public async Task<T> GetAsync(Guid id)
    {
        FilterDefinition<T> filter = FilterBuilder.Eq(entity => entity.Id, id);
        return await DbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await DbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await DbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        FilterDefinition<T> filter = FilterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
        await DbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<T> filter = FilterBuilder.Eq(entity => entity.Id, id);
        await DbCollection.DeleteOneAsync(filter);
    }
}