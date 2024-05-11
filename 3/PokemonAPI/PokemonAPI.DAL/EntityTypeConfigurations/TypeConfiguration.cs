using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DAL.Seeds;
using Type = PokemonAPI.DAL.Entities.Type;

namespace PokemonAPI.DAL.EntityTypeConfigurations;

public class TypeConfiguration : IEntityTypeConfiguration<Type>
{
    public void Configure(EntityTypeBuilder<Type> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Pokemon)
            .WithMany(y => y.Types)
            .HasForeignKey(x => x.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.TypeName).IsRequired().HasMaxLength(50);
        
        builder.HasIndex(x => x.PokemonId);
        if (AppDbContextSeeder.pokemons.Count == 0)
        {
            AppDbContextSeeder.GetAllPokemonsFromApi();
        }
        builder.HasData(AppDbContextSeeder.types);
    }
}