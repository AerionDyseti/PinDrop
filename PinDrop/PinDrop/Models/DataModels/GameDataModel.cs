using System;
using System.Collections.Generic;

namespace PinDrop.Models.DataModels
{
    /// <summary>
    /// Represents a single Game of bowling.
    /// </summary>
    public class GameDataModel
    {

        /// <summary>
        /// Unique ID for this game.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// List of players in this game.
        /// </summary>
        public List<PlayerDataModel> Players { get; set; }

        /// <summary>
        /// Date/Time this game was started.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The current frame this Game is on.
        /// </summary>
        public int? CurrentFrame { get; set; }

        /// <summary>
        /// The current throw inside the frame this game is on.
        /// </summary>
        public int? CurrentThrow { get; set; }


    }

}
