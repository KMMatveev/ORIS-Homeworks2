
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;

namespace PokemonAPI.DAL.Repositories;

public class StatRepository(IDbContext dbContext) : IRepository<Stat, Guid>
{
    public IEnumerable<Stat> GetAllAsync()
    {
        foreach (var stat in dbContext.Stats)
            yield return stat;
    }

    public async Task<Stat> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Stats.FirstOrDefaultAsync(x => x.Id == id,
                         cancellationToken) 
                     ?? throw new Exception($"Stat by id: {id} was not found");

        return result;
    }

    public async Task<Guid> InsertAsync(Stat entity, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Pokemons.AnyAsync(x => x.Id == entity.PokemonId, cancellationToken))
            throw new Exception($"Pokemon with id: {entity.PokemonId} doesn't exist");
        
        if (entity.Id == default)
            entity.Id = Guid.NewGuid();
        else if (await dbContext.Types.AnyAsync(x => x.Id == entity.Id, cancellationToken))
            throw new Exception($"Type with the same id already exist");
            
        await dbContext.Stats.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Guid> UpdateAsync(Stat entity, CancellationToken cancellationToken = default)
    {
        var updateStat = await dbContext.Stats
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
       
        if (updateStat is null)
            throw new Exception($"Type with id {entity.Id} which you want to update was not found");

        updateStat.PokemonId = entity.PokemonId;
        updateStat.StatValue = entity.StatValue;
        updateStat.StatName = entity.StatName;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;    
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var stat = await dbContext.Stats
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (stat is null)
            throw new Exception($"Type with id {id} which you want to delete was not found");

        dbContext.Stats.Remove(stat);
        await dbContext.SaveChangesAsync(cancellationToken);
        return stat.Id;
    }
}