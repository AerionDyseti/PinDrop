using System;
using System.Collections.Generic;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    ///     DTO for displaying information about a specific game.
    /// </summary>
    public class GameViewModel
    {
        /// <summary>
        ///     Unique ID for this game.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The player for this game.
        /// </summary>
        public PlayerViewModel Player { get; set; }

        /// <summary>
        ///     Date/Time this game was started.
        /// </summary>
        public DateTime GameStart { get; set; }

        /// <summary>
        ///     A list of the frames in this game.
        /// </summary>
        public List<FrameViewModel> Frames { get; set; }

        /// <summary>
        ///     The current frame this Game is on.
        /// </summary>
        public int? CurrentFrame { get; set; }

        /// <summary>
        ///     The current throw inside the frame this game is on.
        /// </summary>
        public int? CurrentThrow { get; set; }

        /// <summary>
        ///     Total score for this game.
        /// </summary>
        public int TotalScore { get; set; }
    }
}


/* SAMPLE OUTPUT:
     {
        "id": "c26e23c7-2323-4012-b30b-6c399e179252",
        "gameStart": "2019-07-30T15:01:55.714942+03:00",
        "playerId": "3e5e3270-87db-44c3-b750-30101af4d632",   
        "frames": [
            { "fameNumber": 1, "throws": ["5","/"], "score": 20}, 
            { "fameNumber": 2, "throws": ["X"], "score": 19}, 
            { "fameNumber": 3, "throws": ["1","8"], "score": 9},
            { "fameNumber": 4, "throws": ["6"] } 
        ],
        "currentFrame": 4,
        "currentThrow": 2,        
        "totalScore": 48
    }
 */