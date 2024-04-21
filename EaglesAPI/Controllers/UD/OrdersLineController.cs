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
    public class OrdersLineController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public OrdersLineController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrdersLine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> GetOrdersLines()
        {
          if (_context.OrdersLines == null)
          {
              return NotFound();
          }
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrdersLine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> GetOrdersLine(string id)
        {
          if (_context.OrdersLines == null)
          {
              return NotFound();
          }
            var ordersLine = await _context.OrdersLines.FindAsync(id);

            if (ordersLine == null)
            {
                return NotFound();
            }

            return ordersLine;
        }

        // PUT: api/OrdersLine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdersLine(string id, OrdersLine ordersLine)
        {
            if (id != ordersLine.OrdersLineId)
            {
                return BadRequest();
            }

            _context.Entry(ordersLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersLineExists(id))
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

        // POST: api/OrdersLine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> PostOrdersLine(OrdersLine ordersLine)
        {
          if (_context.OrdersLines == null)
          {
              return Problem("Entity set 'EaglesOracleContext.OrdersLines'  is null.");
          }
            _context.OrdersLines.Add(ordersLine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrdersLineExists(ordersLine.OrdersLineId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrdersLine", new { id = ordersLine.OrdersLineId }, ordersLine);
        }

        // DELETE: api/OrdersLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersLine(string id)
        {
            if (_context.OrdersLines == null)
            {
                return NotFound();
            }
            var ordersLine = await _context.OrdersLines.FindAsync(id);
            if (ordersLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(ordersLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersLineExists(string id)
        {
            return (_context.OrdersLines?.Any(e => e.OrdersLineId == id)).GetValueOrDefault();
        }
    }
}
