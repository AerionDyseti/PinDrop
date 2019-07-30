using System;
using System.Collections.Generic;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    /// DTO for displaying information about a specific game.
    /// </summary>
    public class GameViewModel
    {
    
        /// <summary>
        /// Unique ID for this game.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Date/Time this game was started.
        /// </summary>
        public DateTime GameStart { get; set; }

        /// <summary>
        /// Unique ID for the player whose turn it is.
        /// </summary>
        public Guid CurrentPlayerId { get; set; }
        
        /// <summary>
        /// The scores associated with this game.
        /// </summary>
        public List<GameScoreViewModel> Scores { get; set; }       

    }

}


/* SAMPLE OUTPUT:
     {
        "id": "c26e23c7-2323-4012-b30b-6c399e179252",
        "gameStart": "2019-07-30T15:01:55.714942+03:00",
        "currentPlayerId": "3e5e3270-87db-44c3-b750-30101af4d632",   
        "scores": [{
            "player": {
                "id": "3e5e3270-87db-44c3-b750-30101af4d632",
                "name": "Player1"
            },
            "frames": [
                { "throws": ["5","/"], "score": 20}, 
                { "throws": ["X"], "score": 19}, 
                { "throws": ["1","8"], "score": 9},
                { "throws": ["6"] } 
            ],
            "totalScore": 48,
            "currentFrame": 4,
            "currentThrow": 2
        }]
    }
 */
