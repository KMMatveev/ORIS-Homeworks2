using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.Models.DTOs.Stat;

namespace PokemonAPI.Controllers;

/// <summary>
/// CRUD Controller of Entity Stat
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StatController : CrudBaseController<Stat, Guid, CreateStatDto, UpdateStatDto, ReadStatDto>
{
    /// <inheritdoc cref="CrudBaseController{TEntity,TKey,TCreateDto,TUpdateDto,TReadDto}"/>
    public StatController(IMapper mapper, IRepository<Stat, Guid> repository) : base(mapper, repository)
    {
    }
}