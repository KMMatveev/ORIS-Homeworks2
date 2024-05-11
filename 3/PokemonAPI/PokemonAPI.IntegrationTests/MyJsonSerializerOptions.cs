using System.Text.Json;

namespace PokemonAPI.IntegrationTests;

internal static class MyJsonSerializerOptions
{
    internal static JsonSerializerOptions Options 
        => new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}