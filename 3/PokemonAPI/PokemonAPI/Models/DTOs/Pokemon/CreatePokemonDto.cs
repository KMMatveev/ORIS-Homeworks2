using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Pokemon;

using Pokemon = DAL.Entities.Pokemon;

public class CreatePokemonDto : IMapWith<Pokemon>
{
    public string Name { get; set; }
    
    public string ImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePokemonDto, Pokemon>()
            .ForMember(x => x.Name, 
                opt =>
                    opt.MapFrom(y => y.Name))
            .ForMember(x => x.ImageUrl, 
                opt =>
                    opt.MapFrom(y => y.ImageUrl));
    }
}