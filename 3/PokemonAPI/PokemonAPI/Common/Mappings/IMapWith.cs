using AutoMapper;

namespace PokemonAPI.Common.Mappings;

/// <summary>
/// Responsible for Mapp object to other
/// </summary>
/// <typeparam name="T">Mapping With T</typeparam>
public interface IMapWith<T>
{
    /// <summary>
    /// Mapping from one object to other
    /// </summary>
    /// <param name="profile">Profile</param>
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}