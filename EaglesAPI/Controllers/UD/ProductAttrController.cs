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
    public class ProductAttrController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public ProductAttrController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductAttr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttr>>> GetProductAttrs()
        {
          if (_context.ProductAttrs == null)
          {
              return NotFound();
          }
            return await _context.ProductAttrs.ToListAsync();
        }

        // GET: api/ProductAttr/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAttr>> GetProductAttr(string id)
        {
          if (_context.ProductAttrs == null)
          {
              return NotFound();
          }
            var productAttr = await _context.ProductAttrs.FindAsync(id);

            if (productAttr == null)
            {
                return NotFound();
            }

            return productAttr;
        }

        // PUT: api/ProductAttr/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAttr(string id, ProductAttr productAttr)
        {
            if (id != productAttr.ProductAttrId)
            {
                return BadRequest();
            }

            _context.Entry(productAttr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttrExists(id))
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

        // POST: api/ProductAttr
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductAttr>> PostProductAttr(ProductAttr productAttr)
        {
          if (_context.ProductAttrs == null)
          {
              return Problem("Entity set 'EaglesOracleContext.ProductAttrs'  is null.");
          }
            _context.ProductAttrs.Add(productAttr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductAttr", new { id = productAttr.ProductAttrId }, productAttr);
        }

        // DELETE: api/ProductAttr/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttr(string id)
        {
            if (_context.ProductAttrs == null)
            {
                return NotFound();
            }
            var productAttr = await _context.ProductAttrs.FindAsync(id);
            if (productAttr == null)
            {
                return NotFound();
            }

            _context.ProductAttrs.Remove(productAttr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAttrExists(string id)
        {
            return (_context.ProductAttrs?.Any(e => e.ProductAttrId == id)).GetValueOrDefault();
        }
    }
}
