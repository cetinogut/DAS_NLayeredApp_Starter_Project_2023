using Microsoft.EntityFrameworkCore;
using NLayered.Core.Repositories;
using NLayered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWitCategory()
        {
            //eager loadinng yaptık burada-lazyloadingin ters,
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
