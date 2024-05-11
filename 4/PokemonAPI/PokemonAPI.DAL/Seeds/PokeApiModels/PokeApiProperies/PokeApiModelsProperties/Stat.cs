using System.Text.Json.Serialization;

namespace PokemonAPI.DAL.Seeds.PokeApiModels.PokeApiProperies.PokeApiModelsProperties;

public class Stat
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }
    
    [JsonPropertyName("stat")]
    public StatValue StatValue { get; set; }
}