using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Stat;

using Stat = DAL.Entities.Stat;

public class ReadStatDto : IMapWith<Stat>
{
    public string Name { get; set; }
    
    public int Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Stat, ReadStatDto>()
            .ForMember(x => x.Name,
                opt =>
                    opt.MapFrom(y => y.StatName))
            .ForMember(x => x.Value,
                opt =>
                    opt.MapFrom(y => y.StatValue));
    }
}