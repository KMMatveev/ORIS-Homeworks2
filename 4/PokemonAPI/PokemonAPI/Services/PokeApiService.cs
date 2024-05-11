using Microsoft.EntityFrameworkCore;
using PokemonAPI.DAL;
using PokemonAPI.DAL.Entities;
using PokemonAPI.Exceptions;
using PokemonAPI.Extensions;
using PokemonAPI.Interfaces;

namespace PokemonAPI.Services;

/// <summary>
/// Responsible for returns the pokemons from pokeAPI
/// </summary>
public class PokeApiService : IPokeApiService
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext">Database Context</param>
    public PokeApiService(IDbContext dbContext)
        => _dbContext = dbContext;

    /// <summary>
    /// Returns pokemon by fullName or id
    /// </summary>
    /// <param name="pokemonSearchParameter">Name or Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemon</returns>
    /// <exception cref="ArgumentException">If pokemonSearchParameter is null or white space</exception>
    public async Task<int> GetPokemonIdAsync(string pokemonSearchParameter,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(pokemonSearchParameter))
            throw new ArgumentException($"Empty value was entered in {nameof(GetPokemonIdAsync)}" +
                                        $" method of {nameof(PokeApiService)} service");

        pokemonSearchParameter = pokemonSearchParameter.Trim().ToLower();

        var isSearchParameterId = int.TryParse(pokemonSearchParameter, out _);

        Pokemon resultPokemon;
        if (isSearchParameterId)
            resultPokemon = await _dbContext.Pokemons.FirstOrDefaultAsync(x =>
                    x.Id == int.Parse(pokemonSearchParameter),
                cancellationToken) ?? throw new PokemonNotFoundException(
                $"Pokemon by id: {pokemonSearchParameter} was not found");
        else
            resultPokemon = await _dbContext.Pokemons.FirstOrDefaultAsync(x =>
                                x.Name.ToLower().Equals(pokemonSearchParameter),
                            cancellationToken)
                        ?? throw new PokemonNotFoundException(
                            $"Pokemon by name: {pokemonSearchParameter} was not found");

        return resultPokemon.Id;
    }

    /// <summary>
    /// Returns pokemons count pokemons on page number page with names which includes search value
    /// </summary>
    /// <param name="searchValue">A part of pokemon name</param>
    /// <param name="pokemonsCount">Count of pokemons</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemons List with name which includes searchValue</returns>
    public async Task<List<int>> GetPokemonsIdByFilterAsync(string searchValue = "", int pokemonsCount = 15,
        int pageNumber = 0, CancellationToken cancellationToken = default)
    {
        if (pokemonsCount < 0 || pageNumber < 0)
            throw new ArgumentException(
                $"{nameof(pokemonsCount)}: {pokemonsCount} and" +
                $" {nameof(pageNumber)} {pageNumber} must be more then zero");

        searchValue = searchValue.Trim().ToLower();

        return await _dbContext.Pokemons.GetFilteredPokemonsIdAsync(searchValue, pokemonsCount,
            pageNumber, cancellationToken);
    }

    /// <summary>
    /// Returns pokemons count pokemons on page number page
    /// </summary>
    /// <param name="pokemonsCount">Count of pokemons</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemons List</returns>
    public async Task<List<int>> GetAllPokemonsIdAsync(int pokemonsCount, int pageNumber,
        CancellationToken cancellationToken)
    {
        if (pokemonsCount < 0 || pageNumber < 0)
            throw new ArgumentException(
                $"{nameof(pokemonsCount)}: {pokemonsCount} and" +
                $" {nameof(pageNumber)} {pageNumber} must be more then zero");
        
        return await _dbContext.Pokemons
            .OrderBy(x => x.Id)
            .Select(x => x.Id).Skip(pokemonsCount * pageNumber)
            .Take(pokemonsCount).ToListAsync(cancellationToken);
    }
}