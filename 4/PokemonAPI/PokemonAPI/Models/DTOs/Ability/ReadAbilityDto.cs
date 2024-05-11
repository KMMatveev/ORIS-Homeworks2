using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Ability;

using Ability = DAL.Entities.Ability;

public class ReadAbilityDto : IMapWith<Ability>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Ability, ReadAbilityDto>()
            .ForMember(x => x.Name,
                opt =>
                    opt.MapFrom(y => y.AbilityName));
    }
}