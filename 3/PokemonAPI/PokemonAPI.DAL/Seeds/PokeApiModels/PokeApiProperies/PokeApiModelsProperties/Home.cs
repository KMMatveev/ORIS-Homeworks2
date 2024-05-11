using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Home
{
    [JsonPropertyName("front_default")]
    public Uri? DefaultSpriteUrl { get; set; }
}