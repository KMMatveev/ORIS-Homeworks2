using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.Models.DTOs.Type;

namespace PokemonAPI.Controllers;

using Type = DAL.Entities.Type;

/// <summary>
///  CRUD Controller of Entity Ability
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TypeController : CrudBaseController<Type, Guid, CreateTypeDto, UpdateTypeDto, TypeDto>
{
    /// <inheritdoc cref="CrudBaseController{TEntity,TKey,TCreateDto,TUpdateDto,TReadDto}"/>
    public TypeController(IMapper mapper, IRepository<Type, Guid> repository) : base(mapper, repository)
    {
    }
}