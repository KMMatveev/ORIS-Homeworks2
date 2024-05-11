using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Seeds;

namespace PokemonAPI.DAL.EntityTypeConfigurations;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name);

        builder.HasOne(x => x.Breeding);
        builder.HasMany(x => x.Abilities);
        builder.HasMany(x => x.Types);
        builder.HasMany(x => x.Moves);
        builder.HasMany(x => x.Stats);
        //var seeder = AppDbContextSeeder();
        if(AppDbContextSeeder.pokemons.Count == 0)
        {
            AppDbContextSeeder.GetAllPokemonsFromApi();
        }
        builder.HasData(AppDbContextSeeder.pokemons);
    }
}