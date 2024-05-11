using System.Text.Json.Serialization;
using PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokemonInfoProperties;

namespace PokemonAPI.DAL.Seeds.PokeApiModels;

public class PokemonsInfo
{
    [JsonPropertyName("count")]
    public int PokemonsCount { get; set; }
    
    [JsonPropertyName("results")]
    public List<PokemonInfo> Pokemons { get; set; }
}