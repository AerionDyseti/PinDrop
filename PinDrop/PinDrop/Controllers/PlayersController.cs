using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinDrop.Models.DataModels;
using PinDrop.Models.RequestModels;
using PinDrop.Models.ViewModels;

namespace PinDrop.Controllers
{

    /// <summary>
    /// Controller that handles REST requests for the player entity.
    /// This is standard CRUD stuff.
    /// </summary>
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        /// <summary>
        /// EF Core context acting as repository for all db entities.
        /// </summary>
        private readonly PinDropContext _context;

        /// <summary>
        /// Initiates a new instance of this controller, injecting the EF Core DB Context.
        /// </summary>
        /// <param name="context"></param>
        public PlayersController(PinDropContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public IEnumerable<PlayerViewModel> GetPlayer()
        {
            return _context.Players.Select(p => new PlayerViewModel { Id = p.Id, Name = p.Name });
        }

        // GET: api/Players/2fd80fce-a163-464b-b87d-015c4b7b01c3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(new PlayerViewModel { Id = player.Id, Name = player.Name });
        }

        // PUT: api/Players/2fd80fce-a163-464b-b87d-015c4b7b01c3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer([FromRoute] Guid id, [FromBody] UpdatePlayerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find our player.
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                // In theory, PUT should be idempotent, which means we should create
                // a new object with the provided GUID. For the sake of brevity,
                // I've elected to only allow PUT to be used to update records
                // (which technically makes it act more like PATCH).
                return NotFound();
            }

            // Update properties.
            player.Name = request.Name;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                // If we hit a concurrency exception and the player no longer exists,
                // It must have been deleted while we were working.
                if (!_context.Players.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                    throw;
            }

            return NoContent();
        }

        // POST: api/Players
        [HttpPost]
        public async Task<IActionResult> PostPlayer([FromBody] CreatePlayerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = new PlayerDataModel {Name = request.Name};
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/2fd80fce-a163-464b-b87d-015c4b7b01c3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

    }
}