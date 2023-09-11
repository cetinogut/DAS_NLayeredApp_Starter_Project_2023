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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();// singleordefault eğer aynı idden birden fazla bulursa exception fırlatır, firstordefault bunu yapmaz, o yüzden single daha iyidir.
        }
    }
}
