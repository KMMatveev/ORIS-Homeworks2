namespace PokemonAPI.Interfaces;

/// <summary>
/// Responsible for returns the pokemons from pokeAPI
/// </summary>
public interface IPokeApiService
{
    /// <summary>
    /// Returns pokemon by fullName or id
    /// </summary>
    /// <param name="pokemonSearchParameter">Name or Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemon</returns>
    /// <exception cref="ArgumentException">If pokemonSearchParameter is null or white space</exception>
    public Task<int> GetPokemonIdAsync(string pokemonSearchParameter, CancellationToken cancellationToken);

    /// <summary>
    /// Returns pokemons count pokemons on page number page with names which includes search value
    /// </summary>
    /// <param name="searchValue">A part of pokemon name</param>
    /// <param name="pokemonsCount">Count of pokemons</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemons List with name which includes searchValue</returns>
    public Task<List<int>> GetPokemonsIdByFilterAsync(string searchValue, int pokemonsCount,
        int pageNumber, CancellationToken cancellationToken);

    /// <summary>
    /// Returns pokemons count pokemons on page number page
    /// </summary>
    /// <param name="pokemonsCount">Count of pokemons</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemons List</returns>
    public Task<List<int>> GetAllPokemonsIdAsync(int pokemonsCount, int pageNumber,
        CancellationToken cancellationToken);
}