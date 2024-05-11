using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Type
{
    [JsonPropertyName("type")]
    public TypeValue TypeValue { get; set; }
}