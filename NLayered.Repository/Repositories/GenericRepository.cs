using Microsoft.EntityFrameworkCore;
using NLayered.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context; //sadece inherited sınıflardan gelir.
        private readonly DbSet<T> _dbSet; // DBdeki table
        //readonly yapıyoruz çünkü ya burada create ederken değer atayacağız, ya da constructorda değer atayacağız. biz he pcontructorda değer atamak istiyoruz, başka bir yerde de değerinin değişmesini istemediğimizden "readonly" yaptık

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {

            await _dbSet.AddAsync(entity);

        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable(); //asnotracking ile EF Core'un çekilen datayı memorye almasını önleyip, performansı artırıyoruz.
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {

            //_context.Entry(entity).State = EntityState.Deleted; // bu kod da aynı işi yapar aslında. bu ve delete basit bir işlem olduğundan async a gerek yok.
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
