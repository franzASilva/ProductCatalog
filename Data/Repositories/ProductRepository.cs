using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Models.ViewModels;

namespace ProductCatalog.Repositories
{
    /// <summary>
    /// Repository Pattern.
    /// </summary>
    public class ProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository([FromServices] DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get()
        {
            var products = await _context
                .Product
                .Include(x => x.Category)
                .Select(y => new ProductViewModel
                {
                    Id = y.Id,
                    Title = y.Title,
                    Description = y.Description,
                    Price = y.Price,
                    Quantity = y.Quantity,
                    Image = y.Image,
                    CategoryId = y.Category.Id,
                    Category = y.Category.Title
                })
                .AsNoTracking()
                .ToListAsync();
            return products;
        }

        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Product.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(y => y.Id == id);
            return product;
        }

        public void Save(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }
        
        public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}