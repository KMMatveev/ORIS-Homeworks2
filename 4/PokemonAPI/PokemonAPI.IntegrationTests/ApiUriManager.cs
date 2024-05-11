namespace PokemonAPI.IntegrationTests;

internal static class ApiUriManager
{
    private const string ApiUrl = "https://localhost:44340/";

    private const string GetPokemonByIdOrName = "api/Pokemon/GetPokemonByIdOrName/";

    private const string GetPokemonsByFilter = "api/Pokemon/GetPokemonsByFilter/";

    private const string GetAllPokemons = "api/Pokemon/GetAllPokemons/";

    internal static Uri GetPokemonByIdOrNameUri(string nameOrId) => new($"{ApiUrl}{GetPokemonByIdOrName}{nameOrId}");

    internal static Uri GetPokemonsByFilterUri(string search, int pokemonsCount = 15, int pageNumber = 0) =>
        new($"{ApiUrl}{GetPokemonsByFilter}{search}?pokemonsCount={pokemonsCount}&pageNumber={pageNumber}");

    internal static Uri GetAllPokemonsUri(int pokemonsCount = 15, int pageNumber = 0) =>
        new($"{ApiUrl}{GetAllPokemons}?pokemonsCount={pokemonsCount}&pageNumber={pageNumber}");
}