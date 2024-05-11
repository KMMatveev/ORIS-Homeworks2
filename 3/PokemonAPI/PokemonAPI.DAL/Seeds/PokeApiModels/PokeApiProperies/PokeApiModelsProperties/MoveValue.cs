using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class MoveValue
{
    [JsonPropertyName("name")]
    public string MoveName { get; set; }
}