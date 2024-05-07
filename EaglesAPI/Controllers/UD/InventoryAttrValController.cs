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
    public class InventoryAttrValController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public InventoryAttrValController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/InventoryAttrVal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryAttrVal>>> GetInventoryAttrVals()
        {
          if (_context.InventoryAttrVals == null)
          {
              return NotFound();
          }
            return await _context.InventoryAttrVals.ToListAsync();
        }

        // GET: api/InventoryAttrVal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryAttrVal>> GetInventoryAttrVal(string id)
        {
          if (_context.InventoryAttrVals == null)
          {
              return NotFound();
          }
            var inventoryAttrVal = await _context.InventoryAttrVals.FindAsync(id);

            if (inventoryAttrVal == null)
            {
                return NotFound();
            }

            return inventoryAttrVal;
        }

        // PUT: api/InventoryAttrVal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryAttrVal(string id, InventoryAttrVal inventoryAttrVal)
        {
            if (id != inventoryAttrVal.InventoryAttrValId)
            {
                return BadRequest();
            }

            _context.Entry(inventoryAttrVal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryAttrValExists(id))
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

        // POST: api/InventoryAttrVal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryAttrVal>> PostInventoryAttrVal(InventoryAttrVal inventoryAttrVal)
        {
          if (_context.InventoryAttrVals == null)
          {
              return Problem("Entity set 'EaglesOracleContext.InventoryAttrVals'  is null.");
          }
            _context.InventoryAttrVals.Add(inventoryAttrVal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryAttrVal", new { id = inventoryAttrVal.InventoryAttrValId }, inventoryAttrVal);
        }

        // DELETE: api/InventoryAttrVal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryAttrVal(string id)
        {
            if (_context.InventoryAttrVals == null)
            {
                return NotFound();
            }
            var inventoryAttrVal = await _context.InventoryAttrVals.FindAsync(id);
            if (inventoryAttrVal == null)
            {
                return NotFound();
            }

            _context.InventoryAttrVals.Remove(inventoryAttrVal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryAttrValExists(string id)
        {
            return (_context.InventoryAttrVals?.Any(e => e.InventoryAttrValId == id)).GetValueOrDefault();
        }
    }
}
