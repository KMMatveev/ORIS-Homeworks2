using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Seeds;

namespace PokemonAPI.DAL.EntityTypeConfigurations;

public class MoveConfiguration : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Pokemon)
            .WithMany(y => y.Moves)
            .HasForeignKey(x => x.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.MoveName).IsRequired().HasMaxLength(50);

        builder.HasIndex(x => x.PokemonId);
        //var seeder = new AppDbContextSeeder();
        if (AppDbContextSeeder.pokemons.Count == 0)
        {
            AppDbContextSeeder.GetAllPokemonsFromApi();
        }
        builder.HasData(AppDbContextSeeder.moves);
    }
}