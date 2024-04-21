using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eagles.EF.Data;
using Eagles.EF.Models;

namespace EaglesAPI.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public ProductPriceController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> GetProductPrices()
        {
          if (_context.ProductPrices == null)
          {
              return NotFound();
          }
            return await _context.ProductPrices.ToListAsync();
        }

        // GET: api/ProductPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPrice>> GetProductPrice(string id)
        {
          if (_context.ProductPrices == null)
          {
              return NotFound();
          }
            var productPrice = await _context.ProductPrices.FindAsync(id);

            if (productPrice == null)
            {
                return NotFound();
            }

            return productPrice;
        }

        // PUT: api/ProductPrice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPrice(string id, ProductPrice productPrice)
        {
            if (id != productPrice.ProductPriceId)
            {
                return BadRequest();
            }

            _context.Entry(productPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductPrice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductPrice>> PostProductPrice(ProductPrice productPrice)
        {
          if (_context.ProductPrices == null)
          {
              return Problem("Entity set 'EaglesOracleContext.ProductPrices'  is null.");
          }
            _context.ProductPrices.Add(productPrice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductPriceExists(productPrice.ProductPriceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductPrice", new { id = productPrice.ProductPriceId }, productPrice);
        }

        // DELETE: api/ProductPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductPrice(string id)
        {
            if (_context.ProductPrices == null)
            {
                return NotFound();
            }
            var productPrice = await _context.ProductPrices.FindAsync(id);
            if (productPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(productPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPriceExists(string id)
        {
            return (_context.ProductPrices?.Any(e => e.ProductPriceId == id)).GetValueOrDefault();
        }
    }
}
