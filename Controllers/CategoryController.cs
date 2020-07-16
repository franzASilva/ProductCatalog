using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        [Route("v1/categories")]
        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<IEnumerable<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Category.AsNoTracking().ToListAsync();
            return categories;
        }

        [Route("v1/categories/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Category>> GetById([FromServices] DataContext context, int id)
        {
            var category = await context.Category.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        [Route("v1/categories/{id:int}/products")]
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromServices] DataContext context, int id)
        {
            var products = await context.Product.AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return products;
        }

        [Route("v1/categories")]
        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody]Category category)
        {
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Category.Add(category);
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not create category\r\n{ex.Message}" });
            }
        }

        [Route("v1/categories")]
        [HttpPut]
        public async Task<ActionResult<Category>> Put([FromServices] DataContext context, [FromBody]Category category)
        {
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Category>(category).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return category;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = $"Could not update category\r\n{ex.Message}" });
            }
        }

        [Route("v1/categories/{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<Category>> Delete([FromServices] DataContext context, int id)
        {
            var category = await context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            try
            {
                context.Category.Remove(category);
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Could not remove category\r\n{ex.Message}" });
            }            
        }
    }
}