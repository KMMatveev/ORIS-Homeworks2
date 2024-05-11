using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class StatValue
{
    [JsonPropertyName("name")]
    public string StatName { get; set; }
}