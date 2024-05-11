using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.Interfaces;
using PokemonAPI.Models.DTOs.Pokemon;

namespace PokemonAPI.Controllers;

/// <summary>
/// The controller is responsible for the return of Pokemon
/// </summary>
public class PokemonController :
    CrudBaseController<Pokemon, int, CreatePokemonDto, UpdatePokemonDto, DetailsReadPokemonDto>
{
    private readonly IPokeApiService _pokeApiService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pokeApiService">IPokeApiService</param>
    /// <param name="mapper">IMapper</param>
    /// <param name="pokemonRepository">IRepository{TEntity, TKey}</param>
    /// <inheritdoc cref="CrudBaseController{TEntity,TKey,TCreateDto,TUpdateDto,TReadDto}"/>
    public PokemonController(IPokeApiService pokeApiService, IMapper mapper,
        IRepository<Pokemon, int> pokemonRepository) : base(mapper, pokemonRepository)
    {
        _pokeApiService = pokeApiService;
    }

    /// <summary>
    /// Returns Details Pokemon
    /// </summary>
    /// <param name="pokemonSearchParameter">Name or Id of pokemon</param>
    /// <param name="cancellationToken"></param>
    /// <returns>DetailsPokemonDto</returns>
    /// <exception cref="ArgumentException"></exception>
    [HttpGet("GetPokemonByIdOrName/{pokemonSearchParameter}")]
    public async Task<DetailsReadPokemonDto> GetPokemonByIdOrName(string pokemonSearchParameter,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(pokemonSearchParameter))
            throw new ArgumentException($"Empty value was entered in {nameof(GetPokemonByIdOrName)}" +
                                        $" method of {nameof(PokemonController)} endpoint");

        var id = await _pokeApiService.GetPokemonIdAsync(pokemonSearchParameter, cancellationToken);

        var result = await Repository.GetByIdAsync(id, cancellationToken);
        
        return Mapper.Map<DetailsReadPokemonDto>(result);
    }

    /// <summary>
    /// Returns pokemons list on specific page by name which includes search
    /// </summary>
    /// <param name="search">A part of pokemon name</param>
    /// <param name="pokemonsCount">Number of pokemons list result length</param>
    /// <param name="pageNumber">Number of pokemons page</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemons list with length = pokemonsCount on page = pageNumber by name which includes search</returns>
    [HttpGet("GetPokemonsByFilter/{search}")]
    public async Task<IEnumerable<ReadPokemonDto>> GetPokemonsByFilter(string search,
        [FromQuery] int pokemonsCount,
        [FromQuery] int pageNumber,
        CancellationToken cancellationToken)
    {
        var pokemonsId =
            await _pokeApiService.GetPokemonsIdByFilterAsync(search, pokemonsCount, pageNumber, cancellationToken);

        var result = new List<Pokemon?>();
        foreach (var id in pokemonsId)
        {
            var pokemon =  await Repository.GetByIdAsync(id, cancellationToken);
            result.Add(pokemon);
        }

        return result.Select(Mapper.Map<ReadPokemonDto>);
    }

    /// <summary>
    /// Returns all pokemons list on specific page
    /// </summary>
    /// <param name="pokemonsCount">Number of pokemons list result length</param>
    /// <param name="pageNumber">Number of pokemons page</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Pokemons list with length = pokemonsCount on page = pageNumber</returns>
    [HttpGet("GetAllPokemons")]
    public async Task<IEnumerable<ReadPokemonDto>> GetAllPokemons([FromQuery] int pokemonsCount,
        [FromQuery] int pageNumber,
        CancellationToken cancellationToken)
    {
        var pokemonsId = await _pokeApiService.GetAllPokemonsIdAsync(pokemonsCount, pageNumber, cancellationToken);

        var result = new List<Pokemon?>();
        foreach (var id in pokemonsId)
        {
            var pokemon = await Repository.GetByIdAsync(id, cancellationToken);
            result.Add(pokemon);
        }

        return result.Select(Mapper.Map<ReadPokemonDto>);
    }
}