using AutoMapper;

namespace JfService.Balance.Application.Mapping
{
    /// <summary>
    /// Создает профиль глобального маппинга для преобразования экземпляра класса, 
    /// релизующего данный интерфейс, в указанный тип <see cref="T"/>.
    /// </summary>
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
