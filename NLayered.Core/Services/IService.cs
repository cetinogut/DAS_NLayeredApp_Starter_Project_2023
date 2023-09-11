using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Core.Services
{
    public interface IService<T> where T : class // generic repository'dek idönüş tiplerinden değişiklik olacaktır.
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(); //IGenericRep'dan biraz farklı olsun diye IEnumerable yaptık.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);// Bunların IGenericRepo'da asyncleri yok ama buradan DB'ye giderken tıkanma olmasın diye asyncler eklenecek.
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);


    }
}
