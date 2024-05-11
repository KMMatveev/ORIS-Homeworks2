using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokemonInfoProperties;

public class PokemonInfo
{
    [JsonPropertyName("name")]
    public string PokemonName { get; set; }
    
    [JsonPropertyName("url")]
    public Uri PokemonUrl { get; set; }
}