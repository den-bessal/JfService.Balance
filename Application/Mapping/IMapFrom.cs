using AutoMapper;

namespace JfService.Balance.Application.Mapping
{
    /// <summary>
    /// Создает профиль глобального маппинга для преобразования из указанного типа <see cref="T"/> 
    /// в экземпляр класса, реализующего данный интерфейс.
    /// </summary>
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}