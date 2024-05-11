using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;

namespace PokemonAPI.DAL.Repositories;

public class BreedingRepository(IDbContext dbContext) : IRepository<Breeding, Guid>
{
    public IEnumerable<Breeding> GetAllAsync()
    {
        foreach (var breeding in dbContext.Breedings)
            yield return breeding;
    }

    public async Task<Breeding> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Breedings.FirstOrDefaultAsync(x => x.Id == id,
                         cancellationToken) 
                     ?? throw new Exception($"Breeding by id: {id} was not found");

        return result;   
    }

    public async Task<Guid> InsertAsync(Breeding entity, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Pokemons.AnyAsync(x => x.Id == entity.PokemonId, cancellationToken))
            throw new Exception($"Pokemon with id: {entity.PokemonId} doesn't exist");
        
        if (entity.Id == default)
            entity.Id = Guid.NewGuid();
        else if (await dbContext.Breedings.AnyAsync(x => x.Id == entity.Id, cancellationToken))
            throw new Exception($"Breeding with the same id already exist");
            
        await dbContext.Breedings.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Guid> UpdateAsync(Breeding entity, CancellationToken cancellationToken = default)
    {
        var updateBreeding = await dbContext.Breedings
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
       
        if (updateBreeding is null)
            throw new Exception($"Breeding with id {entity.Id} which you want to update was not found");

        updateBreeding.PokemonId = entity.PokemonId;
        updateBreeding.Height = entity.Height;
        updateBreeding.Weight = entity.Weight;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;      
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var breeding = await dbContext.Breedings
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (breeding is null)
            throw new Exception($"Breeding with id {id} which you want to delete was not found");

        dbContext.Breedings.Remove(breeding);
        await dbContext.SaveChangesAsync(cancellationToken);
        return breeding.Id;    
    }
}