namespace PokemonAPI.DAL.Entities;

public class Ability
{
    public Guid Id { get; set; }

    public int PokemonId { get; set; }
    
    public Pokemon? Pokemon { get; set; }

    public string AbilityName { get; set; } = default!;
}