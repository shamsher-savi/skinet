using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;

        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var products = await _context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var brands = await _context.ProductBrands.ToListAsync();
            return brands;
        }

        public async Task<IReadOnlyList<ProductType>> GetGroductTypesAsync()
        {
            var types = await _context.ProductTypes.ToListAsync();
            return types;
        }
    }
}