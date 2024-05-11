using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Stat;

using Stat = DAL.Entities.Stat;

public class CreateStatDto : IMapWith<Stat>
{
    public int PokemonId { get; set; }
    
    public string Name { get; set; }
    
    public int Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateStatDto, Stat>()
            .ForMember(x => x.StatName,
                opt =>
                    opt.MapFrom(y => y.Name))
            .ForMember(x => x.StatValue,
                opt =>
                    opt.MapFrom(y => y.Value))
            .ForMember(x => x.PokemonId,
                opt =>
                    opt.MapFrom(y => y.PokemonId));
    }
}