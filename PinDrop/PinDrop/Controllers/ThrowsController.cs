using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinDrop.Models.DataModels;

namespace PinDrop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThrowsController : ControllerBase
    {
        private readonly PinDropContext _context;

        public ThrowsController(PinDropContext context)
        {
            _context = context;
        }

        // GET: api/Throws
        [HttpGet]
        public IEnumerable<ThrowDataModel> GetThrows()
        {
            return _context.Throws;
        }

        // GET: api/Throws/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThrowDataModel([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var throwDataModel = await _context.Throws.FindAsync(id);

            if (throwDataModel == null)
            {
                return NotFound();
            }

            return Ok(throwDataModel);
        }

        // PUT: api/Throws/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThrowDataModel([FromRoute] Guid id, [FromBody] ThrowDataModel throwDataModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != throwDataModel.GameId)
            {
                return BadRequest();
            }

            _context.Entry(throwDataModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThrowDataModelExists(id))
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

        // POST: api/Throws
        [HttpPost]
        public async Task<IActionResult> PostThrowDataModel([FromBody] ThrowDataModel throwDataModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Throws.Add(throwDataModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ThrowDataModelExists(throwDataModel.GameId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetThrowDataModel", new { id = throwDataModel.GameId }, throwDataModel);
        }

        // DELETE: api/Throws/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThrowDataModel([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var throwDataModel = await _context.Throws.FindAsync(id);
            if (throwDataModel == null)
            {
                return NotFound();
            }

            _context.Throws.Remove(throwDataModel);
            await _context.SaveChangesAsync();

            return Ok(throwDataModel);
        }

        private bool ThrowDataModelExists(Guid id)
        {
            return _context.Throws.Any(e => e.GameId == id);
        }
    }
}