using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Pokemon;

using Pokemon = DAL.Entities.Pokemon;

public class ReadPokemonDto: IMapWith<Pokemon>
{
    /// <summary>
    /// Pokemon Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Pokemon Name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Pokemon image url
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Pokemon Types List
    /// </summary>
    public List<string> Types { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pokemon, ReadPokemonDto>()
            .ForMember(dto => dto.Id,
                opt =>
                    opt.MapFrom(pokemon => pokemon.Id))
            .ForMember(dto => dto.Name,
                opt =>
                    opt.MapFrom(pokemon => pokemon.Name))
            .ForMember(dto => dto.ImageUrl, 
                opt => 
                    opt.MapFrom(pokemon => pokemon.ImageUrl))
            .ForMember(dto => dto.Types,
                opt =>
                    opt.MapFrom(pokemon => pokemon.Types.Select(type => type.TypeName)));
    }
}