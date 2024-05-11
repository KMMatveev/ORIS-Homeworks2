namespace PokemonAPI.DAL.Entities;

public class Type
{
    public Guid Id { get; set; }
    
    public int PokemonId { get; set; }
    
    public Pokemon? Pokemon { get; set; }

    public string TypeName { get; set; } = default!;
}