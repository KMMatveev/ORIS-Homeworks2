using System.Text.Json;
using PokemonAPI.Models.DTOs.Pokemon;

namespace PokemonAPI.IntegrationTests.PokeApiControllerTests;

[TestClass]
public class GetPokemonsByFilterTest
{
    [TestMethod("CaseIgnored")]
    [DataRow("BULB", "bULb")]
    public async Task GetPokemonsByFilter_CaseIgnored(string filter1, string filter2)
    {
        // Arrange
        var requestUri1 = ApiUriManager.GetPokemonsByFilterUri(filter1, 10, 0);
        var requestUri2 = ApiUriManager.GetPokemonsByFilterUri(filter2, 10, 0);

        // Act
        var response1 = await ApiMessageSender.SendRequest(requestUri1);
        var response2 = await ApiMessageSender.SendRequest(requestUri2);

        var pokemonsListJson1 = await response1.Content.ReadAsStringAsync();
        var pokemonsListJson2 = await response2.Content.ReadAsStringAsync();

        var pokemonsList1 =
            JsonSerializer.Deserialize<List<ReadPokemonDto>>(pokemonsListJson1, MyJsonSerializerOptions.Options);
        var pokemonsList2 =
            JsonSerializer.Deserialize<List<ReadPokemonDto>>(pokemonsListJson2, MyJsonSerializerOptions.Options);

        // Assert
        var equalsPokemons =
            pokemonsList1.Join(pokemonsList2,
                poke1 => poke1.Id,
                poke2 => poke2.Id,
                (poke1, poke2) => poke1).ToList();

        Assert.IsTrue(equalsPokemons.Count == pokemonsList1.Count && equalsPokemons.Count == pokemonsList2.Count);
    }

    [TestMethod("ReturnsZeroPokemonsIfTheyAreNotFound")]
    [DataRow("sdflkjsdflkdjsfklsdnf")]
    public async Task GetPokemonsByFilter_ReturnsZeroPokemonsIfTheyAreNotFound(string nonExistentName)
    {
        // Arrange
        var requestUri = ApiUriManager.GetPokemonsByFilterUri(nonExistentName, 15, 0);

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);
        var pokemonsListJson = await response.Content.ReadAsStringAsync();
        var pokemonsList =
            JsonSerializer.Deserialize<List<ReadPokemonDto>>(pokemonsListJson, MyJsonSerializerOptions.Options);

        // Assert
        Assert.IsTrue(pokemonsList.Count == 0);
    }

    [TestMethod("FilterWorksCorrectly")]
    [DataRow("bu")]
    public async Task GetPokemonsByFilter_FilterWorksCorrectly(string searchParameter)
    {
        // Arrange
        var requestUri = ApiUriManager.GetPokemonsByFilterUri(searchParameter, 15, 0);

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);
        var pokemonsListJson = await response.Content.ReadAsStringAsync();
        var pokemonsList =
            JsonSerializer.Deserialize<List<ReadPokemonDto>>(pokemonsListJson, MyJsonSerializerOptions.Options);

        // Assert
        var rightPokemonsList = pokemonsList.Where(pokemon =>
            pokemon.Name.Equals(searchParameter, StringComparison.OrdinalIgnoreCase) ||
            pokemon.Name.StartsWith(searchParameter, StringComparison.OrdinalIgnoreCase) ||
            pokemon.Name.Contains(searchParameter, StringComparison.OrdinalIgnoreCase)).ToList();

        Assert.IsTrue(rightPokemonsList.Count == pokemonsList.Count);
    }
}