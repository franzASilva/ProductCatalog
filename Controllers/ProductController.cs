using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Models.ViewModels;
using ProductCatalog.Repositories;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        [Route("v1/products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get([FromServices] DataContext context)
        {
            var products = await context
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

        [Route("v1/products/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Product.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(y => y.Id == id);
            return product;
        }

        [Route("v2/products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get([FromServices] ProductRepository repository)
        {
            return await repository.Get();
        }

        [Route("v2/products/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetById([FromServices] ProductRepository repository, int id)
        {
            return await repository.GetById(id);
        }

        [Route("v1/products")]
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] ProductViewModel newProduct)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Title = newProduct.Title,
                    Description = newProduct.Description,
                    Price = newProduct.Price,
                    Quantity = newProduct.Quantity,
                    Image = newProduct.Image,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    CategoryId = newProduct.CategoryId,
                };

                context.Product.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("v1/products")] 
        [HttpPut]
        public async Task<ActionResult<Product>> Put([FromServices] DataContext context, [FromBody] ProductViewModel productUpdated)
        {
            if (ModelState.IsValid)
            {
                var product = await context.Product.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == productUpdated.Id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found" });
                }

                product.Title = productUpdated.Title;
                product.Description = productUpdated.Description;
                product.Price = productUpdated.Price;
                product.Quantity = productUpdated.Quantity;
                product.Image = productUpdated.Image;
                product.LastUpdateDate = DateTime.Now;
                product.CategoryId = productUpdated.CategoryId;

                try
                {
                    context.Entry<Product>(product).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return product;
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Could not update product\r\n{ex.Message}" });
                } 
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
