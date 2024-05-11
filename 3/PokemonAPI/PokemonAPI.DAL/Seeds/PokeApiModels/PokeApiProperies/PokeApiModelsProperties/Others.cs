using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Others
{
    [JsonPropertyName("home")]
    public Home HomeSprites { get; set; }
}