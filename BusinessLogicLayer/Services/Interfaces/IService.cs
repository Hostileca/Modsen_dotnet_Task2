using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IService<TEntity, TEntityReadDto, TEntityCreateDto,TEntityUpdateDto>
    {
        Task<IEnumerable<TEntityReadDto>> GetAll();
        TEntityReadDto GetByPredicate(Expression<Func<TEntityReadDto, bool>> predicate);
        Task CreateItem(TEntityCreateDto itemCreateDto);
        Task UpdateItem(TEntityUpdateDto itemUpdateDto);
        Task DeleteItem(TEntity item);
        void SaveChanges();
    }
}
