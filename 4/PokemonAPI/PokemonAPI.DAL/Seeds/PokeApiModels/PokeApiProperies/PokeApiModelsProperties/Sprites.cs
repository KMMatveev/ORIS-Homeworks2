using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Sprites
{
    [JsonPropertyName("other")]
    public Others OtherSprites { get; set; }
}