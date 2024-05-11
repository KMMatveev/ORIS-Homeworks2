using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class AbilityValue
{
    [JsonPropertyName("name")]
    public string AbilityName { get; set; }
}