namespace PokemonAPI.DAL.Repositories;

public interface IRepository<TEntity, TKey>
{
    public IEnumerable<TEntity> GetAllAsync();

    public Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    public Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task<TKey> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public Task<TKey> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}