using Microsoft.EntityFrameworkCore;
using Type = PokemonAPI.DAL.Entities.Type;

namespace PokemonAPI.DAL.Repositories;

public class TypeRepository(IDbContext dbContext) : IRepository<Type, Guid>
{
    public IEnumerable<Type> GetAllAsync()
    {
        foreach (var type in dbContext.Types)
            yield return type;
    }

    public async Task<Type> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Types.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken) 
                 ?? throw new Exception($"Type by id: {id} was not found");

        return result;
    }

    public async Task<Guid> InsertAsync(Type entity, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Pokemons.AnyAsync(x => x.Id == entity.PokemonId, cancellationToken))
            throw new Exception($"Pokemon with id: {entity.PokemonId} doesn't exist");
        
        if (entity.Id == default)
            entity.Id = Guid.NewGuid();
        else if (await dbContext.Types.AnyAsync(x => x.Id == entity.Id, cancellationToken))
            throw new Exception($"Type with the same id already exist");
            
        await dbContext.Types.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
    
    public async Task<Guid> UpdateAsync(Type entity, CancellationToken cancellationToken = default)
    {
        var updateType = await dbContext.Types
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
       
        if (updateType is null)
            throw new Exception($"Type with id {entity.Id} which you want to update was not found");

        updateType.TypeName = entity.TypeName;
        updateType.PokemonId = entity.PokemonId;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var type = await dbContext.Types
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (type is null)
            throw new Exception($"Type with id {id} which you want to delete was not found");

        dbContext.Types.Remove(type);
        await dbContext.SaveChangesAsync(cancellationToken);
        return type.Id;
    }
}