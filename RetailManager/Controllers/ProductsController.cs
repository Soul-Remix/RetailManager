using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailManager.Data;
using RetailManager.DTO;
using RetailManager.Models;

namespace RetailManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Product>> GetProducts()
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();
        return products;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductDto model)
    {
        var product = new Product();
        product.Description = model.Description;
        product.Name = model.Name;
        product.RetailPrice = model.RetailPrice;
        product.QuantityInStock = model.QuantityInStock;
        // Remove after switching to postgreSql
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(ProductDto product, int id)
    {
        var productToUpdate = await _context.Products.FindAsync(id);

        if (productToUpdate == null)
        {
            return NotFound();
        }

        productToUpdate.Description = product.Description;
        productToUpdate.Name = product.Name;
        productToUpdate.RetailPrice = product.RetailPrice;
        productToUpdate.QuantityInStock = product.QuantityInStock;

        _context.Update(productToUpdate);

        try
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}