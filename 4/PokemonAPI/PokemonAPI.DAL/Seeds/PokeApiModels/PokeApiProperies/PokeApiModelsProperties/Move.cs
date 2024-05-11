using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Move
{
    [JsonPropertyName("move")]
    public MoveValue MoveValue { get; set; }
}