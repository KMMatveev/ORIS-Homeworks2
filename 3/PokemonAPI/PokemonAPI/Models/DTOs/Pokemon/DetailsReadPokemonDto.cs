using AutoMapper;
using PokemonAPI.Common.Mappings;
using PokemonAPI.Models.Properties.DtosProperties;

namespace PokemonAPI.Models.DTOs.Pokemon;

using Pokemon = DAL.Entities.Pokemon;

public class DetailsReadPokemonDto: ReadPokemonDto, IMapWith<Pokemon>
{
    /// <summary>
    /// Pokemon Height in decimeters
    /// </summary>
    public int Height { get; set; }
        
    /// <summary>
    /// Pokemon weight in kilograms
    /// </summary>
    public int Weight { get; set; }
    
    /// <summary>
    /// Pokemon abilities list
    /// </summary>
    public List<string> Abilities { get; set; }
    
    /// <summary>
    /// Pokemon Moves List
    /// </summary>
    public List<string> Moves { get; set; }
    
    /// <summary>
    /// Pokemon Stats List
    /// </summary>
    public List<StatDto> Stats { get; set; }

    /// <inheritdoc />
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pokemon, DetailsReadPokemonDto>()
            .ForMember(dto => dto.Id,
                opt =>
                    opt.MapFrom(dto => dto.Id))
            .ForMember(dto => dto.Name,
                opt =>
                    opt.MapFrom(dto => dto.Name))
            .ForMember(dto => dto.ImageUrl, 
                opt => 
                    opt.MapFrom(pokemon => pokemon.ImageUrl))
            .ForMember(dto => dto.Abilities,
                opt =>
                    opt.MapFrom(dto => 
                        dto.Abilities.Select(ability => ability.AbilityName).ToList()))
            .ForMember(dto => dto.Types,
                opt =>
                    opt.MapFrom(dto =>
                        dto.Types.Select(type => type.TypeName).ToList()))
            .ForMember(dto => dto.Moves,
                opt =>
                    opt.MapFrom(dto =>
                        dto.Moves.Select(move => move.MoveName).ToList()))
            .ForMember(dto => dto.Stats,
                opt =>
                    opt.MapFrom(pokemon => pokemon.Stats.Select(stat =>
                        new StatDto(stat.StatName, stat.StatValue)).ToList()))
            .ForMember(dto => dto.Height,
                opt =>
                    opt.MapFrom(pokemon => pokemon.Breeding.Height))
            .ForMember(dto => dto.Weight,
                opt =>
                    opt.MapFrom(pokemon => pokemon.Breeding.Weight));
    }
}