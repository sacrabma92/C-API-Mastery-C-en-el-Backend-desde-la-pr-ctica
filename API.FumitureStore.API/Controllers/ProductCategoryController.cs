using API.FumitureStore.Data;
using API.FumitureStore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FumitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly APIFurnitureStoreContext _context;

        public ProductCategoryController(APIFurnitureStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryDetails(int id)
        {
            var category = await _context.ProductCategories.FirstOrDefaultAsync( p => p.Id == id);

            if(category == null) 
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostProductCategory(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Post", productCategory.Id, productCategory);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(ProductCategory productCategory)
        {
            if(productCategory== null) return NotFound();

            _context.ProductCategories.Remove(productCategory);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
