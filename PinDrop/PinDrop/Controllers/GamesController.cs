using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinDrop.Models.DataModels;
using PinDrop.Models.RequestModels;
using PinDrop.Models.ViewModels;

namespace PinDrop.Controllers
{
    /// <summary>
    ///     REST Controller that handles actions related to a Game,
    ///     such as viewing the score or creating a new game.
    /// </summary>
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        /// <summary>
        ///     Ef Core db context being used as repository and unit-of-work.
        /// </summary>
        private readonly PinDropContext _context;

        /// <summary>
        ///     Initializes a new instance of this controller, injecting the EF Core DB Context.
        /// </summary>
        /// <param name="context"></param>
        public GamesController(PinDropContext context)
        {
            _context = context;
        }


        // GET: api/Games
        [HttpGet]
        public IEnumerable<GameViewModel> GetGames()
        {
            return _context.Games
                .Include(g => g.Player)
                .Include(g => g.Frames)
                .Select(g => ViewModelFactory.CreateGameViewModel(g));
        }

        // GET: api/Games/3e5e3270-87db-44c3-b750-30101af4d632
        [HttpGet("{gameId}")]
        [ProducesResponseType(typeof(GameViewModel), 200)]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            GameDataModel gameDataModel = await _context.Games
                .Include(g => g.Player)
                .Include(g => g.Frames)
                .SingleOrDefaultAsync(g => g.Id == gameId);

            if (gameDataModel == null) return NotFound();

            return Ok(ViewModelFactory.CreateGameViewModel(gameDataModel));
        }

        // POST: api/Games
        [HttpPost]
        [ProducesResponseType(typeof(GameViewModel), 201)]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            PlayerDataModel player = await _context.Players.FindAsync(request.PlayerId);
            if (player == null) return BadRequest($"No player with Id {request.PlayerId} exists.");

            // Create new Game entry in DB.
            GameDataModel gameDataModel = new GameDataModel
                {Player = player, CreationDate = DateTime.Now, Frames = new List<FrameDataModel>()};
            _context.Games.Add(gameDataModel);
            await _context.SaveChangesAsync();

            // Create first Frame entry for the new Game.
            FrameDataModel firstFrameDataModel = new FrameDataModel
            {
                CreationDate = DateTime.Now,
                GameId = gameDataModel.Id,
                FrameNumber = 1
            };
            _context.Frames.Add(firstFrameDataModel);
            await _context.SaveChangesAsync();

            // Update new Game entry with the new Frame and throw.
            gameDataModel.Frames.Add(firstFrameDataModel);
            gameDataModel.CurrentFrameId = gameDataModel.Frames.First().Id;
            gameDataModel.CurrentThrow = 1;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new {gameId = gameDataModel.Id},
                ViewModelFactory.CreateGameViewModel(gameDataModel));
        }


        // POST: api/Games/3e5e3270-87db-44c3-b750-30101af4d632/Throws
        [HttpPost("{gameId}/throws")]
        [ProducesResponseType(typeof(GameViewModel), 201)]
        public async Task<IActionResult> AddThrow([FromRoute] Guid gameId, [FromBody] CreateThrowRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (request.PinsDropped > 10) return BadRequest("A maximum of 10 pins may be dropped.");

            GameDataModel thisGame = await _context.Games
                .Include(g => g.Player)
                .Include(g => g.Frames)
                .SingleOrDefaultAsync(g => g.Id == gameId);
            if (thisGame == null) return BadRequest($"No game with Id {gameId} exists.");
            if (thisGame.IsFinished) return BadRequest("This game is complete, no more throws allowed.");

            // Set current throw to the value we just received.
            FrameDataModel currentFrame = await _context.Frames.SingleAsync(f => f.Id == thisGame.CurrentFrameId);

            // First Throw
            if (thisGame.CurrentThrow == 1)
            {
                currentFrame.FirstThrow = request.PinsDropped;
                if (request.PinsDropped == 10)
                {
                    currentFrame.IsStrike = true;
                    currentFrame.IsFinished = true;
                }
            }

            // Second Throw
            else if (thisGame.CurrentThrow == 2)
            {
                currentFrame.SecondThrow = request.PinsDropped;
                // Mark if Spare.
                currentFrame.IsSpare = currentFrame.FirstThrow + request.PinsDropped == 10;

                // Non-Final Frames are finished at this point, and can score if not a strike or spare.
                if (currentFrame.FrameNumber < 10)
                {
                    currentFrame.IsFinished = true;
                }
                // Final Frame.
                else
                {
                    currentFrame.IsFinished = !(currentFrame.IsStrike || currentFrame.IsSpare);
                    thisGame.IsFinished = !(currentFrame.IsStrike || currentFrame.IsSpare);
                }
            }

            // Third Throw (Final Frame Only)
            else if (thisGame.CurrentThrow == 3)
            {
                currentFrame.ThirdThrow = request.PinsDropped;
                currentFrame.IsFinished = true;
                thisGame.IsFinished = true;
            }


            // If we need a new frame, create one and set it to throw 1 of that new frame.
            Boolean needsNewFrame = currentFrame.IsFinished && currentFrame.FrameNumber < 10;
            if (needsNewFrame)
            {
                FrameDataModel newFrame = new FrameDataModel
                {
                    CreationDate = DateTime.Now,
                    GameId = thisGame.Id,
                    FrameNumber = currentFrame.FrameNumber + 1
                };
                _context.Frames.Add(newFrame);
                await _context.SaveChangesAsync();

                thisGame.Frames.Add(newFrame);
                thisGame.CurrentFrameId = newFrame.Id;
                thisGame.CurrentThrow = 1;
            }
            else if (!thisGame.IsFinished)
            {
                thisGame.CurrentThrow++;
            }

            await _context.SaveChangesAsync();


            return Ok(ViewModelFactory.CreateGameViewModel(thisGame));
        }
    }
}