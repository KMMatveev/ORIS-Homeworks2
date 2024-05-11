using AutoMapper;
using PokemonAPI.Common.Mappings;

namespace PokemonAPI.Models.DTOs.Move;

using Move = DAL.Entities.Move;

public class ReadMoveDto : IMapWith<Move>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Move, ReadMoveDto>()
            .ForMember(x => x.Name,
                opt =>
                    opt.MapFrom(y => y.MoveName));
    }
}