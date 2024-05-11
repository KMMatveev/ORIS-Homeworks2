using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Type;

using Type = DAL.Entities.Type;

public class TypeDto : IMapWith<Type>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Type, TypeDto>()
            .ForMember(x => x.Name,
                opt =>
                    opt.MapFrom(y => y.TypeName));
    }
}