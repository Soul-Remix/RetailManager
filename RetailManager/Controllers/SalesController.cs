using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailManager.Data;
using RetailManager.DTO;
using RetailManager.Models;

namespace RetailManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            var saleToUpdate = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (saleToUpdate == null)
            {
                return NotFound();
            }

            _context.Entry(sale).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(SaleDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var sale = new Sale
            {
                SubTotal = model.SubTotal,
                Tax = model.Tax,
                Total = model.Total,
                CashierId = userId,
            };

            _context.Sales.Add(sale);
            foreach (var item in model.Cart)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    return BadRequest();
                }

                var price = product.RetailPrice * item.Quantity;
                var tax = (price * 8) / 100;

                var saleDetail = new SaleDetail
                {
                    PurchasePrice = price,
                    Tax = product.IsTaxable ? tax : 0,
                    Quantity = item.Quantity,
                    SaleId = sale.Id,
                    ProductId = product.Id,
                };

                product.QuantityInStock -= item.Quantity;
                _context.Products.Update(product);
                _context.SaleDetails.Add(saleDetail);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }
    }
}