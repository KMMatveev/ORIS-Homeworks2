using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class TypeValue
{
    [JsonPropertyName("name")]
    public string TypeName { get; set; }
}