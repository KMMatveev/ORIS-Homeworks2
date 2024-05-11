using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Entities;
using PokemonAPI.DAL.Repositories;
using PokemonAPI.Models.DTOs.Move;

namespace PokemonAPI.Controllers;

/// <summary>
/// CRUD Controller of Entity Move
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MoveController : CrudBaseController<Move, Guid, CreateMoveDto, UpdateMoveDto, ReadMoveDto>
{
    /// <inheritdoc cref="CrudBaseController{TEntity,TKey,TCreateDto,TUpdateDto,TReadDto}"/>
    public MoveController(IMapper mapper, IRepository<Move, Guid> repository) : base(mapper, repository)
    {
    }
}