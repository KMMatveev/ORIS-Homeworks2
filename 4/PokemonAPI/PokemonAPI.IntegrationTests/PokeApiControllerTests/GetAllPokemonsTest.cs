using System.Net;
using System.Text.Json;
using PokemonAPI.Models.DTOs.Pokemon;

namespace PokemonAPI.IntegrationTests.PokeApiControllerTests;

[TestClass]
public class GetAllPokemonsTest
{
    [TestMethod("PokemonsCount")]
    [DataRow(15, 0)]
    [DataRow(10, 2)]
    public async Task GetAllPokemons_PokemonsCount(int pokemonsCount, int pageNumber)
    {
        // Arrange
        var requestUri = ApiUriManager.GetAllPokemonsUri(pokemonsCount, pageNumber);

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);
        var pokemonsListJson = await response.Content.ReadAsStringAsync();
        var pokemonsList =
            JsonSerializer.Deserialize<List<ReadPokemonDto>>(pokemonsListJson, MyJsonSerializerOptions.Options);

        // Assert
        Assert.IsTrue(pokemonsList.Count == pokemonsCount);
    }

    [TestMethod("PageNumberOrPokemonsCountLessThenZero")]
    [DataRow(-1, 2)]
    [DataRow(10, -5)]
    public async Task GetAllPokemons_PageNumberOrPokemonsCountLessThenZero(int pokemonsCount, int pageNumber)
    {
        // Arrange
        var requestUri = ApiUriManager.GetAllPokemonsUri(pokemonsCount, pageNumber);

        // Act
        var response = await ApiMessageSender.SendRequest(requestUri);

        // Assert
        Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
    }

    [TestMethod("DifferentPokemonOnDifferentPages")]
    [DataRow(15, 0, 15, 1)]
    public async Task GetAllPokemons_DifferentPokemonOnDifferentPages(int pokemonsCount1, int pageNumber1,
        int pokemonsCount2, int pageNumber2)
    {
        // Arrange
        var requestUri1 = ApiUriManager.GetAllPokemonsUri(pokemonsCount1, pageNumber1);
        var requestUri2 = ApiUriManager.GetAllPokemonsUri(pokemonsCount2, pageNumber2);

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

        Assert.IsTrue(equalsPokemons.Count == 0);
    }
}