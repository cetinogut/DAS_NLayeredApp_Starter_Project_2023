using System.Linq.Expressions;

namespace NLayered.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll(); //IQueryable ile dönünce yazılan sorgu doğrudan DB'ye gitmez, ToList, ToListAsync yazınca DB'ye gider, Bu performans için daha iyidir.
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //IQuaryable ile orderby vs gibi bir sorgu yapsak bile daha DB'ye gitmez. IQuaryable yerine ToList yaparsak where(x0>x.id == 5).ToLisst deyince bütün veriyi gidip DB'den getirir. OrderBy vs gele ndata üzerindne yapılır ki bu durum performans açısından istenmez.
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity); // async yapısı yok, EF Core update ve remove uzun sürmez o yüzden async yok ama add uzun sürebilir, o yüzden async vardır.
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
