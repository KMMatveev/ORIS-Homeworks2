using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Seeds.PokeApiModels;
using Type = PokemonAPI.DAL.Entities.Type;

namespace PokemonAPI.DAL.Seeds;

public class AppDbContextSeeder /*: ISeeder*/
{
    public static List<Pokemon> pokemons = new List<Pokemon>();
    public static List<Ability> abilities = new List<Ability>();
    public static List<Breeding> breedings = new List<Breeding>();
    public static List<Move> moves = new List<Move>();
    public static List<Type> types = new List<Type>();
    public static List<Stat> statuses = new List<Stat>();  

    private static readonly Uri PokemonsInfoUri = new("https://pokeapi.co/api/v2/pokemon?limit=10000&offset=0");

    public static void GetAllPokemonsFromApi()
    {
        //var pokemonsNamesFromDb = _dbContext.Pokemons.Select(pokemon => pokemon.Name)
        //    .ToHashSet();
        //var newPokemons = info.Pokemons.Where(x =>
        //    !pokemonsNamesFromDb.Contains(x.PokemonName));
        var newPokemons=  GetFromApi<PokemonsInfo>(PokemonsInfoUri);
        var i = 0;
        foreach (var pokemonInfo in newPokemons.Pokemons)
        {
           //if (i == 10) break;
            var pokemon =  GetFromApi<PokemonFromApi>(pokemonInfo.PokemonUrl);
            pokemons.Add(MapFromPokemonFromApi(pokemon));
            abilities.AddRange(MapAbilitiesFromApi(pokemon));
            breedings.Add(MapBreedingFromApi(pokemon));
            moves.AddRange(MapMovesFromApi(pokemon));
            types.AddRange(MapTypesFromApi(pokemon));
            statuses.AddRange(MapStatsFromApi(pokemon));
            //await _dbContext.Pokemons.AddAsync(dbPokemon, cancellationToken).ConfigureAwait(false);
            //_logger.Log(LogLevel.Information, $"Added {pokemon.Name} with id: {pokemon.Id}");
            i++;
        }

        //return result;
        //await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private static string SendGetRequest(Uri requestUri)
    {
        var _client = new HttpClient();
        var message = new HttpRequestMessage();
        message.RequestUri = requestUri;
        message.Method = HttpMethod.Get;

        var httpResponse = _client.Send(message);

        if (httpResponse.StatusCode != HttpStatusCode.OK)
            throw new HttpRequestException(
                $"{(int)httpResponse.StatusCode} by {requestUri} in {nameof(SendGetRequest)}");

        return httpResponse.Content.ReadAsStringAsync().Result;
    }

    private static T GetFromApi<T>(Uri requestUri)
    {
        var resultJson = SendGetRequest(requestUri);

        var result = JsonSerializer.Deserialize<T>(resultJson) ?? throw new NullReferenceException(
            $"Null {nameof(PokemonsInfo)} was returned from {requestUri} in {nameof(GetFromApi)} service");

        return result;
    }

    private static List<Type> MapTypesFromApi(PokemonFromApi pokemon)
    {
        return pokemon.Types.Select(x =>
            new Type
            {
                Id = Guid.NewGuid(),
                PokemonId = pokemon.Id,
                TypeName = x.TypeValue.TypeName
            }
        ).ToList();
    }
    private static List<Move> MapMovesFromApi(PokemonFromApi pokemon)
    {
        return pokemon.Moves.Select(x =>
            new Move
            {
                Id = Guid.NewGuid(),
                PokemonId = pokemon.Id,
                MoveName = x.MoveValue.MoveName
            }
        ).ToList();
    }

    private static List<Ability> MapAbilitiesFromApi(PokemonFromApi pokemon)
    {
        return pokemon.Abilities.Select(x =>
            new Ability
            {
                Id = Guid.NewGuid(),
                PokemonId = pokemon.Id,
                AbilityName = x.AbilityValue.AbilityName
            }).ToList();
    }

    private static Breeding MapBreedingFromApi(PokemonFromApi pokemon)
    { 
        return new Breeding
        {
            Id = Guid.NewGuid(),
            PokemonId = pokemon.Id,
            Weight = pokemon.Weight,
            Height = pokemon.Height
        };
    }

    private static List<Stat> MapStatsFromApi(PokemonFromApi pokemon)
    {
        return pokemon.Stats.Select(x =>
            new Stat
            {
                Id = Guid.NewGuid(),
                PokemonId = pokemon.Id,
                StatValue = x.BaseStat,
                StatName = x.StatValue.StatName
            }
        ).ToList();
    }

    private static Pokemon MapFromPokemonFromApi(PokemonFromApi pokemon)
    {
        var result = new Pokemon
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            ImageUrl = pokemon.Sprites.OtherSprites.HomeSprites.DefaultSpriteUrl?.ToString() ?? string.Empty
        };

        //result.Breeding = new Breeding
        //{
        //    Id = Guid.NewGuid(),
        //    PokemonId = result.Id,
        //    Pokemon = result,
        //    Weight = pokemon.Weight,
        //    Height = pokemon.Height
        //};

        //result.Abilities = pokemon.Abilities.Select(x =>
        //    new Ability
        //    {
        //        Id = Guid.NewGuid(),
        //        PokemonId = result.Id,
        //        Pokemon = result,
        //        AbilityName = x.AbilityValue.AbilityName
        //    }).ToList();

        //result.Moves = pokemon.Moves.Select(x =>
        //    new Move
        //    {
        //        Id = Guid.NewGuid(),
        //        PokemonId = result.Id,
        //        Pokemon = result,
        //        MoveName = x.MoveValue.MoveName
        //    }
        //).ToList();

        //result.Types = pokemon.Types.Select(x =>
        //    new Type
        //    {
        //        Id = Guid.NewGuid(),
        //        PokemonId = result.Id,
        //        Pokemon = result,
        //        TypeName = x.TypeValue.TypeName
        //    }
        //).ToList();

        //result.Stats = pokemon.Stats.Select(x =>
        //    new Stat
        //    {
        //        Id = Guid.NewGuid(),
        //        PokemonId = result.Id,
        //        Pokemon = result,
        //        StatValue = x.BaseStat,
        //        StatName = x.StatValue.StatName
        //    }
        //).ToList();

        return result;
    }
}