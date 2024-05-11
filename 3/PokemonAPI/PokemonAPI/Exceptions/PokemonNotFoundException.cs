namespace PokemonAPI.Exceptions;

/// <summary>
/// If pokemon not was found
/// </summary>
public class PokemonNotFoundException: Exception
{
    /// <inheritdoc />
    public PokemonNotFoundException(string message) : base(message)
    { }
}