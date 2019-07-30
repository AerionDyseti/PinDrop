using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinDrop.Models.DataModels;
using PinDrop.Models.RequestModels;

namespace PinDrop.Controllers
{
    /// <summary>
    /// REST Controller that handles actions related to a Game,
    /// such as viewing the score or creating a new game.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        // Ef Core db context being used as repository and unit-of-work.
        private readonly PinDropContext _context;

        /// <summary>
        /// Initializes a new instance of this controller, injecting the EF Core DB Context.
        /// </summary>
        /// <param name="context"></param>
        public GamesController(PinDropContext context)
        {
            _context = context;
        }


        // GET: api/Games
        [HttpGet]
        public IEnumerable<GameDataModel> GetGames()
        {
            return _context.Games;
        }

        // GET: api/Games/3e5e3270-87db-44c3-b750-30101af4d632
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameDataModel([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gameDataModel = await _context.Games.FindAsync(id);

            if (gameDataModel == null)
            {
                return NotFound();
            }

            return Ok(gameDataModel);
        }

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> PostGameDataModel([FromBody] CreateGameRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var players = new List<PlayerDataModel>();
            foreach (Guid id in request.PlayerIds)
            {
                var player = await _context.Players.FindAsync(id);
                if (player == null) { return BadRequest($"No player with Id {id} exists."); }
                players.Add(player);
            }

            var gameDataModel = new GameDataModel
            {
                Players = players,
                CreationDate = DateTime.Now,
                CurrentFrame = 1,
                CurrentThrow = 1
            };

            _context.Games.Add(gameDataModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameDataModel", new { id = gameDataModel.Id }, gameDataModel);
        }

    }

}