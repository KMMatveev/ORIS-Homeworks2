using System.Net;
using PokemonAPI.Models.DTOs.Pokemon;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PokemonAPI.IntegrationTests.PokeApiControllerTests;

[TestClass]
public class GetPokemonByIdOrNameTest
{
    [TestMethod("NotFoundPokemon")]
    public async Task GetPokemonByIdOrName_NotFoundPokemon()
    {
        // Arrange
        var requestUri = ApiUriManager.GetPokemonByIdOrNameUri(10000000.ToString());

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);

        // Assert
        Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
    }

    [TestMethod("ReturnedRightPokemonById")]
    public async Task GetPokemonByIdOrName_ReturnedRightPokemonById()
    {
        // Arrange
        var requestUri = ApiUriManager.GetPokemonByIdOrNameUri(1.ToString());

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);
        var pokemonJson = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<DetailsReadPokemonDto>(pokemonJson, MyJsonSerializerOptions.Options);

        // Assert
        Assert.IsTrue(pokemon is not null && pokemon.Id == 1);
    }

    [TestMethod("ReturnedRightPokemonByName")]
    public async Task GetPokemonByIdOrName_ReturnedRightPokemonByName()
    {
        // Arrange
        var pokemonName = "bulbasaur";
        var requestUri = ApiUriManager.GetPokemonByIdOrNameUri(pokemonName);

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);
        var pokemonJson = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<DetailsReadPokemonDto>(pokemonJson, MyJsonSerializerOptions.Options);

        // Assert
        Assert.IsTrue(pokemon is not null && pokemon.Name.Equals(pokemonName, StringComparison.OrdinalIgnoreCase));
    }
}