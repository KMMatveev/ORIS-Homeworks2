using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Breeding;

using Breeding = DAL.Entities.Breeding;

public class CreateBreedingDto : IMapWith<Breeding>
{
    public int PokemonId { get; set; }
    
    public int Height { get; set; }
    
    public int Weigth { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBreedingDto, Breeding>()
            .ForMember(x => x.PokemonId,
                opt =>
                    opt.MapFrom(y => y.PokemonId))
            .ForMember(x => x.Height,
                opt =>
                    opt.MapFrom(y => y.Height))
            .ForMember(x => x.Weight,
                opt =>
                    opt.MapFrom(y => y.Weigth));
    }
}