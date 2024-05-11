using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Type;

using Type = DAL.Entities.Type;

public class UpdateTypeDto: IMapWith<Type>
{
    public int PokemonId { get; set; }
    
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateTypeDto, Type>()
            .ForMember(x => x.TypeName,
                opt =>
                    opt.MapFrom(y => y.Name))
            .ForMember(x => x.PokemonId,
                opt =>
                    opt.MapFrom(y => y.PokemonId));
    }
}