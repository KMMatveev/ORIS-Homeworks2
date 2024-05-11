using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;
using Type = PokemonAPI.DAL.Entities.Type;

namespace PokemonAPI.DAL;

public interface IDbContext
{
    public DbSet<Breeding> Breedings { get; set; }

    public DbSet<Pokemon> Pokemons { get; set; }
    
    public DbSet<Stat> Stats { get; set; }

    public DbSet<Ability> Abilities { get; set; }
    
    public DbSet<Type> Types { get; set; }

    public DbSet<Move> Moves { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}