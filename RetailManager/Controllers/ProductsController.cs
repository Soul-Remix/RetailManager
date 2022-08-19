using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailManager.Data;
using RetailManager.DTO;
using RetailManager.Models;

namespace RetailManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Product>> GetProducts(string? search)
    {
        var products = _context.Products.AsNoTracking()
            .Where(p => p.IsArchived == false);

        if (!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Name.Contains(search));
        }

        var result = await products.ToListAsync();

        return result;
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
        product.IsTaxable = model.IsTaxable;
        product.IsArchived = model.IsArchived;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto product)
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
        productToUpdate.IsTaxable = product.IsTaxable;
        productToUpdate.IsArchived = product.IsArchived;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        product.IsArchived = true;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}