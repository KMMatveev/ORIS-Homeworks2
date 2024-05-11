using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;

namespace PokemonAPI.DAL.Repositories;

public class MoveRepository(IDbContext dbContext) : IRepository<Move, Guid>
{
    public IEnumerable<Move> GetAllAsync()
    {
        foreach (var move in dbContext.Moves)
            yield return move;
    }

    public async Task<Move> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await dbContext.Moves.FirstOrDefaultAsync(x => x.Id == id,
                         cancellationToken) 
                     ?? throw new Exception($"Move by id: {id} was not found");

        return result;    
    }

    public async Task<Guid> InsertAsync(Move entity, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Pokemons.AnyAsync(x => x.Id == entity.PokemonId, cancellationToken))
            throw new Exception($"Pokemon with id: {entity.PokemonId} doesn't exist");
        
        if (entity.Id == default)
            entity.Id = Guid.NewGuid();
        else if (await dbContext.Moves.AnyAsync(x => x.Id == entity.Id, cancellationToken))
            throw new Exception($"Move with the same id already exist");
            
        await dbContext.Moves.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;    
    }

    public async Task<Guid> UpdateAsync(Move entity, CancellationToken cancellationToken = default)
    {
        var updateMove = await dbContext.Moves
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
       
        if (updateMove is null)
            throw new Exception($"Move with id {entity.Id} which you want to update was not found");

        updateMove.PokemonId = entity.PokemonId;
        updateMove.MoveName = entity.MoveName;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;    
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var move = await dbContext.Moves
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (move is null)
            throw new Exception($"Move with id {id} which you want to delete was not found");

        dbContext.Moves.Remove(move);
        await dbContext.SaveChangesAsync(cancellationToken);
        return move.Id;
    }
}