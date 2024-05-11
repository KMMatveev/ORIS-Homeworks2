using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.EntityTypeConfigurations;
using Type = PokemonAPI.DAL.Entities.Type;

namespace PokemonAPI.DAL;

public class DbContext: Microsoft.EntityFrameworkCore.DbContext, IDbContext
{
    public DbSet<Breeding> Breedings { get; set; } = default!;

    public DbSet<Pokemon> Pokemons { get; set; } = default!;
    
    public DbSet<Stat> Stats { get; set; } = default!;

    public DbSet<Ability> Abilities { get; set; } = default!;
    
    public DbSet<Type> Types { get; set; } = default!;

    public DbSet<Move> Moves { get; set; } = default!;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="options"></param>
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AbilityConfiguration());
        modelBuilder.ApplyConfiguration(new BreedingConfiguration());
        modelBuilder.ApplyConfiguration(new MoveConfiguration());
        modelBuilder.ApplyConfiguration(new PokemonConfiguration());
        modelBuilder.ApplyConfiguration(new StatConfiguration());
        modelBuilder.ApplyConfiguration(new TypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}