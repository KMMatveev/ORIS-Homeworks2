using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.Models.DTOs.Breeding;

namespace PokemonAPI.Controllers;

/// <summary>
/// CRUD Controller of Entity Breeding
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BreedingController :
    CrudBaseController<Breeding, Guid, CreateBreedingDto, UpdateBreedingDto, ReadBreedingDto>
{
    /// <inheritdoc cref="CrudBaseController{TEntity,TKey,TCreateDto,TUpdateDto,TReadDto}"/>
    public BreedingController(IMapper mapper, IRepository<Breeding, Guid> repository) : base(mapper, repository)
    {
    }
}