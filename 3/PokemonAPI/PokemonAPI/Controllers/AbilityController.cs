using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.Models.DTOs.Ability;

namespace PokemonAPI.Controllers;

/// <summary>
/// CRUD Controller of Entity Ability
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AbilityController :
    CrudBaseController<Ability, Guid, CreateAbilityDto, UpdateAbilityDto, ReadAbilityDto>
{
    /// <inheritdoc cref="CrudBaseController{TEntity,TKey,TCreateDto,TUpdateDto,TReadDto}"/>
    public AbilityController(IMapper mapper, IRepository<Ability, Guid> repository) : base(mapper, repository)
    {
    }
}
