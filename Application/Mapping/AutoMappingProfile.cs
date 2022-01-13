using AutoMapper;
using JfService.Balance.Application.Extenions;
using System;
using System.Linq;
using System.Reflection;

namespace JfService.Balance.Application.Mapping
{
    /// <summary>
    /// Глобальный профиль маппинга для классов реализующих интерфейсы <see cref="IMapFrom{T}"/> и <see cref="IMapTo{T}"/>.
    /// </summary>
    public class AutoMappingProfile : Profile
    {
        private const string MappingMethodName = "Mapping";

        public AutoMappingProfile() => ApplyMappingFromAssembly(Assembly.GetEntryAssembly());

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetWithReferenced()
                                .SelectMany(x => x.GetExportedTypes())
                                .Where(type => type.GetInterfaces()
                                                   .Any(i => i.IsGenericType
                                                             && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
                                                             || i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(MappingMethodName)
                    ?? type.GetInterface("IMapFrom`1")?.GetMethod(MappingMethodName)
                    ?? type.GetInterface("IMapTo`1")?.GetMethod(MappingMethodName);

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}