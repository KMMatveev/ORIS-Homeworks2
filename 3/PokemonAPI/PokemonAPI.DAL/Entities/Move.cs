namespace PokemonAPI.DAL.Entities;

public class Move
{
    public Guid Id { get; set; }
    
    public int PokemonId { get; set; }
    
    public Pokemon? Pokemon { get; set; }

    public string MoveName { get; set; } = default!;
}