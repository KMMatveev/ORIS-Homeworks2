using System.Reflection;
using AutoMapper;

namespace PokemonAPI.Common.Mappings;


/// <summary>
/// Assembly Mapping Profile for Mapping
/// </summary>
public class AssemblyMappingProfile : Profile
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="assembly">Assembly of proj</param>
    public AssemblyMappingProfile(Assembly assembly) =>
        ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes().Where(type =>
                type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new[] { this });
        }
    }
}