namespace PokemonAPI.DAL.Entities;

public class Breeding
{
    public Guid Id { get; set; }
    
    public int PokemonId { get; set; }
    
    public Pokemon? Pokemon { get; set; }
    
    public int Weight { get; set; }
    
    public int Height { get; set; }
}