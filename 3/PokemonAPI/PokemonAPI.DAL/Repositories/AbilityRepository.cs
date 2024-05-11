using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;

namespace PokemonAPI.DAL.Repositories;

public class AbilityRepository(IDbContext dbContext) : IRepository<Ability, Guid>
{
    public IEnumerable<Ability> GetAllAsync()
    {
        foreach (var ability in dbContext.Abilities)
            yield return ability;
    }

    public async Task<Ability> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Abilities.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken) 
                 ?? throw new Exception($"Ability by id: {id} was not found");

        return result;
    }

    public async Task<Guid> InsertAsync(Ability entity, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Pokemons.AnyAsync(x => x.Id == entity.PokemonId, cancellationToken))
            throw new Exception($"Pokemon with id: {entity.PokemonId} doesn't exist");
        
        if (entity.Id == default)
            entity.Id = Guid.NewGuid();
        else if (await dbContext.Abilities.AnyAsync(x => x.Id == entity.Id, cancellationToken))
            throw new Exception($"Ability with the same id: {entity.Id} already exist");
            
        await dbContext.Abilities.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
    
    public async Task<Guid> UpdateAsync(Ability entity, CancellationToken cancellationToken = default)
    {
        var updateAbility = await dbContext.Abilities
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
       
        if (updateAbility is null)
            throw new Exception($"Ability with id {entity.Id} which you want to update was not found");

        updateAbility.PokemonId = entity.PokemonId;
        updateAbility.AbilityName = entity.AbilityName;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ability = await dbContext.Abilities
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (ability is null)
            throw new Exception($"Ability with id {id} which you want to delete was not found");

        dbContext.Abilities.Remove(ability);
        await dbContext.SaveChangesAsync(cancellationToken);
        return ability.Id;
    }
}