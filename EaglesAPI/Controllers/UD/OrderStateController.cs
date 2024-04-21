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
    public class OrderStateController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public OrderStateController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> GetOrderStates()
        {
          if (_context.OrderStates == null)
          {
              return NotFound();
          }
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> GetOrderState(string id)
        {
          if (_context.OrderStates == null)
          {
              return NotFound();
          }
            var orderState = await _context.OrderStates.FindAsync(id);

            if (orderState == null)
            {
                return NotFound();
            }

            return orderState;
        }

        // PUT: api/OrderState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderState(string id, OrderState orderState)
        {
            if (id != orderState.OrderStateId)
            {
                return BadRequest();
            }

            _context.Entry(orderState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStateExists(id))
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

        // POST: api/OrderState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderState>> PostOrderState(OrderState orderState)
        {
          if (_context.OrderStates == null)
          {
              return Problem("Entity set 'EaglesOracleContext.OrderStates'  is null.");
          }
            _context.OrderStates.Add(orderState);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderStateExists(orderState.OrderStateId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderState", new { id = orderState.OrderStateId }, orderState);
        }

        // DELETE: api/OrderState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderState(string id)
        {
            if (_context.OrderStates == null)
            {
                return NotFound();
            }
            var orderState = await _context.OrderStates.FindAsync(id);
            if (orderState == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(orderState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStateExists(string id)
        {
            return (_context.OrderStates?.Any(e => e.OrderStateId == id)).GetValueOrDefault();
        }
    }
}
