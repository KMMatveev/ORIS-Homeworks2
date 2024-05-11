using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DAL.Repositories;

namespace PokemonAPI.Controllers;

/// <summary>
/// Base CRUD Controller of DB Entities
/// </summary>
/// <typeparam name="TEntity">Entity in DB</typeparam>
/// <typeparam name="TKey">Entity Primary Key</typeparam>
/// <typeparam name="TCreateDto">Create DTO</typeparam>
/// <typeparam name="TUpdateDto">Update DTO</typeparam>
/// <typeparam name="TReadDto">Read DTO</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudBaseController<TEntity, TKey, TCreateDto, TUpdateDto, TReadDto> : ControllerBase
{
    /// <summary>
    /// Mapper
    /// </summary>
    protected readonly IMapper Mapper;

    /// <summary>
    /// Repository of Entity
    /// </summary>
    protected readonly IRepository<TEntity, TKey> Repository;
    
    /// <summary>
    /// Constructor with services for CRUD
    /// </summary>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="repository"><see cref="IRepository{TEntity,TKey}"/></param>
    protected CrudBaseController(IMapper mapper, IRepository<TEntity, TKey> repository)
    {
        Mapper = mapper;
        Repository = repository;
    }

    /// <summary>
    /// Create Entity
    /// </summary>
    /// <param name="createDto">Create DTO</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID of created Entity</returns>
    [HttpPost("Create")]
    public async Task<TKey> Create([FromBody] TCreateDto createDto, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TEntity>(createDto);
        return await Repository.InsertAsync(entity, cancellationToken);
    }
    
    /// <summary>
    ///  Read Entity
    /// </summary>
    /// <param name="id">ID of Entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>TReadDTO</returns>
    [HttpGet("Read/{id}")]
    public async Task<TReadDto> Read(TKey id, CancellationToken cancellationToken)
    {
        var ability =  await Repository.GetByIdAsync(id, cancellationToken);
        return Mapper.Map<TReadDto>(ability);
    }
    
    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="updateDto">Update DTO</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID of updated entity</returns>
    [HttpPut("Update")]
    public async Task<TKey> Update([FromBody] TUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TEntity>(updateDto);
        return await Repository.UpdateAsync(entity, cancellationToken);
    }
    
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="id">Id of entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ID of deleted entity</returns>
    [HttpDelete("Delete")]
    public async Task<TKey> Delete(TKey id, CancellationToken cancellationToken) 
        =>  await Repository.DeleteAsync(id, cancellationToken);
}