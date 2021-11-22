using Microsoft.EntityFrameworkCore;
using Project.Core.Models;
using Project.Core.Repositories;
using Project.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public ProductRepository(DbContext context):base(context)
        {

        }
        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            var product = await _appDbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == productId);
            return product;
        }
    }
}
