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
    public class InventoryStateController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public InventoryStateController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/InventoryState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryState>>> GetInventoryStates()
        {
          if (_context.InventoryStates == null)
          {
              return NotFound();
          }
            return await _context.InventoryStates.ToListAsync();
        }

        // GET: api/InventoryState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryState>> GetInventoryState(string id)
        {
          if (_context.InventoryStates == null)
          {
              return NotFound();
          }
            var inventoryState = await _context.InventoryStates.FindAsync(id);

            if (inventoryState == null)
            {
                return NotFound();
            }

            return inventoryState;
        }

        // PUT: api/InventoryState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryState(string id, InventoryState inventoryState)
        {
            if (id != inventoryState.InventoryStateId)
            {
                return BadRequest();
            }

            _context.Entry(inventoryState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryStateExists(id))
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

        // POST: api/InventoryState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryState>> PostInventoryState(InventoryState inventoryState)
        {
          if (_context.InventoryStates == null)
          {
              return Problem("Entity set 'EaglesOracleContext.InventoryStates'  is null.");
          }
            _context.InventoryStates.Add(inventoryState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryState", new { id = inventoryState.InventoryStateId }, inventoryState);
        }

        // DELETE: api/InventoryState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryState(string id)
        {
            if (_context.InventoryStates == null)
            {
                return NotFound();
            }
            var inventoryState = await _context.InventoryStates.FindAsync(id);
            if (inventoryState == null)
            {
                return NotFound();
            }

            _context.InventoryStates.Remove(inventoryState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryStateExists(string id)
        {
            return (_context.InventoryStates?.Any(e => e.InventoryStateId == id)).GetValueOrDefault();
        }
    }
}
