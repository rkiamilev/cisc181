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
    public class AddressTypeController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public AddressTypeController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/AddressType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> GetAddressTypes()
        {
          if (_context.AddressTypes == null)
          {
              return NotFound();
          }
            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/AddressType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> GetAddressType(string id)
        {
          if (_context.AddressTypes == null)
          {
              return NotFound();
          }
            var addressType = await _context.AddressTypes.FindAsync(id);

            if (addressType == null)
            {
                return NotFound();
            }

            return addressType;
        }

        // PUT: api/AddressType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressType(string id, AddressType addressType)
        {
            if (id != addressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(addressType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST: api/AddressType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressType>> PostAddressType(AddressType addressType)
        {
          if (_context.AddressTypes == null)
          {
              return Problem("Entity set 'EaglesOracleContext.AddressTypes'  is null.");
          }
            _context.AddressTypes.Add(addressType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AddressTypeExists(addressType.AddressTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAddressType", new { id = addressType.AddressTypeId }, addressType);
        }

        // DELETE: api/AddressType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressType(string id)
        {
            if (_context.AddressTypes == null)
            {
                return NotFound();
            }
            var addressType = await _context.AddressTypes.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(addressType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressTypeExists(string id)
        {
            return (_context.AddressTypes?.Any(e => e.AddressTypeId == id)).GetValueOrDefault();
        }
    }
}
