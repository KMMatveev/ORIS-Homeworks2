using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Breeding;

using Breeding = DAL.Entities.Breeding;

public class ReadBreedingDto : IMapWith<Breeding>
{
    public int Heigth { get; set; }
    
    public int Weigth { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Breeding, ReadBreedingDto>()
            .ForMember(x => x.Heigth,
                opt =>
                    opt.MapFrom(y => y.Height))
            .ForMember(x => x.Weigth,
                opt =>
                    opt.MapFrom(y => y.Weight));
    }
}