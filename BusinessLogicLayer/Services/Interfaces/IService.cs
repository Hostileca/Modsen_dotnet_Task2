using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IService<TEntity, TEntityReadDto, TEntityCreateDto,TEntityUpdateDto>
    {
        Task<IEnumerable<TEntityReadDto>> GetAllAsync();
        Task<TEntityReadDto> GetByPredicateAsync(Expression<Func<TEntityReadDto, bool>> predicate);
        Task CreateItemAsync(TEntityCreateDto itemCreateDto);
        Task UpdateItemAsync(TEntityUpdateDto itemUpdateDto);
        Task DeleteItemAsync(TEntity item);
        Task SaveChangesAsync();
    }
}
