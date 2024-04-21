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
    public class CustomerAddressController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public CustomerAddressController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAddress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> GetCustomerAddresses()
        {
          if (_context.CustomerAddresses == null)
          {
              return NotFound();
          }
            return await _context.CustomerAddresses.ToListAsync();
        }

        // GET: api/CustomerAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> GetCustomerAddress(string id)
        {
          if (_context.CustomerAddresses == null)
          {
              return NotFound();
          }
            var customerAddress = await _context.CustomerAddresses.FindAsync(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return customerAddress;
        }

        // PUT: api/CustomerAddress/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAddress(string id, CustomerAddress customerAddress)
        {
            if (id != customerAddress.CustomerAddressId)
            {
                return BadRequest();
            }

            _context.Entry(customerAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
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

        // POST: api/CustomerAddress
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> PostCustomerAddress(CustomerAddress customerAddress)
        {
          if (_context.CustomerAddresses == null)
          {
              return Problem("Entity set 'EaglesOracleContext.CustomerAddresses'  is null.");
          }
            _context.CustomerAddresses.Add(customerAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerAddressExists(customerAddress.CustomerAddressId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerAddress", new { id = customerAddress.CustomerAddressId }, customerAddress);
        }

        // DELETE: api/CustomerAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAddress(string id)
        {
            if (_context.CustomerAddresses == null)
            {
                return NotFound();
            }
            var customerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(customerAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerAddressExists(string id)
        {
            return (_context.CustomerAddresses?.Any(e => e.CustomerAddressId == id)).GetValueOrDefault();
        }
    }
}
