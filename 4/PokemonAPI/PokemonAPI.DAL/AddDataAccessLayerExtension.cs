using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.DAL.Seeds;
using Type = PokemonAPI.DAL.Entities.Type;

namespace PokemonAPI.DAL;

public static class AddDataAccessLayerExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<ISeeder, AppDbContextSeeder>();

        services.AddScoped<IRepository<Pokemon, int>, PokemonRepository>();
        services.AddScoped<IRepository<Ability, Guid>, AbilityRepository>();
        services.AddScoped<IRepository<Breeding, Guid>, BreedingRepository>();
        services.AddScoped<IRepository<Move, Guid>, MoveRepository>();
        services.AddScoped<IRepository<Type, Guid>, TypeRepository>();
        services.AddScoped<IRepository<Stat, Guid>, StatRepository>();

        return services.AddDbContext<IDbContext, DbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}