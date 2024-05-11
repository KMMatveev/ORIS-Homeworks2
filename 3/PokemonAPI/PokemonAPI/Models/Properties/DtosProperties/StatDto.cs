namespace PokemonAPI.Models.Properties.DtosProperties;

public class StatDto
{
    public StatDto(string statName, int statValue)
    {
        StatName = statName;
        StatValue = statValue;
    }

    /// <summary>
    /// Pokemon Stat Name
    /// </summary>
    public string StatName { get; set; }
    
    /// <summary>
    /// Pokemon Stat Value
    /// </summary>
    public int StatValue { get; set; }
}