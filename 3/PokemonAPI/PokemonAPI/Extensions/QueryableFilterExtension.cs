using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL.Entities;

namespace PokemonAPI.Extensions;

/// <summary>
/// Extension for filtering Queryable Collection of Pokemons
/// </summary>
public static class QueryableFilterExtension
{
    /// <summary>
    /// Filtering DbSet{Pokemons} and converts it to List with {pokemonsNumber} pokemons on {page} page
    /// </summary>
    /// <param name="pokemons">DbSet{Pokemons}</param>
    /// <param name="filter">Key name of filtering</param>
    /// <param name="count">Count of pokemons</param>
    /// <param name="page">Count of page</param>
    /// <param name="cancellationToken"></param>
    /// <returns>List with filtering pokemons</returns>
    public static async Task<List<int>> GetFilteredPokemonsIdAsync(this IQueryable<Pokemon> pokemons, string filter,
        int count, int page, CancellationToken cancellationToken = default)
    {
        filter = filter.ToLower();
        var skip = count * page;

        var fullNameMatch = pokemons
            .Where(pokemon => pokemon.Name.ToLower().Equals(filter))
            .Select(x => x.Id)
            .OrderBy(x => x)
            .Skip(skip)
            .Take(count);

        var startsWithNameMatch = pokemons
            .Where(pokemon => pokemon.Name.ToLower().StartsWith(filter) && !pokemon.Name.ToLower().Equals(filter))
            .Select(x => x.Id)
            .OrderBy(x => x)
            .Skip(skip)
            .Take(count - await fullNameMatch.CountAsync());

        var containsNameMatch = pokemons
            .Where(pokemon => pokemon.Name.ToLower().Contains(filter) 
                              && !pokemon.Name.ToLower().StartsWith(filter)
                              && !pokemon.Name.ToLower().Equals(filter))
            .Select(x => x.Id)
            .OrderBy(x => x)
            .Skip(skip)
            .Take(count - await fullNameMatch.CountAsync() - await startsWithNameMatch.CountAsync());

        return await fullNameMatch
            .Concat(startsWithNameMatch)
            .Concat(containsNameMatch)
            .ToListAsync(cancellationToken);
    }
}