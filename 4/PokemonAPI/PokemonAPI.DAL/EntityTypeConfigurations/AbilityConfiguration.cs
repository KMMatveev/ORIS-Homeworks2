using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Seeds;

namespace PokemonAPI.DAL.EntityTypeConfigurations;

public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
{
    public void Configure(EntityTypeBuilder<Ability> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Pokemon)
            .WithMany(y => y.Abilities)
            .HasForeignKey(x => x.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.AbilityName).IsRequired().HasMaxLength(50);
        
        builder.HasIndex(x => x.PokemonId);
        if (AppDbContextSeeder.pokemons.Count == 0)
        {
            AppDbContextSeeder.GetAllPokemonsFromApi();
        }
        builder.HasData(AppDbContextSeeder.abilities);
    }
}