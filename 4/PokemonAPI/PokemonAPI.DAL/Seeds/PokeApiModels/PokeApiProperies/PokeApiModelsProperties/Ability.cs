using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Ability
{
    [JsonPropertyName("ability")]
    public AbilityValue AbilityValue { get; set; }
}