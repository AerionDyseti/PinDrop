using System;
using System.Collections.Generic;

namespace PinDrop.Models.DataModels
{
    /// <summary>
    ///     Represents a single Game of bowling.
    /// </summary>
    public class GameDataModel
    {
        /// <summary>
        ///     Unique ID for this game.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     List of players in this game.
        /// </summary>
        public PlayerDataModel Player { get; set; }

        /// <summary>
        ///     The Throws made in this game.
        /// </summary>
        public List<FrameDataModel> Frames { get; set; }

        /// <summary>
        ///     Date/Time this game was started.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        ///     The current frame this Game is on.
        /// </summary>
        public Guid CurrentFrameId { get; set; }

        /// <summary>
        ///     The current throw inside the frame this game is on.
        /// </summary>
        public int? CurrentThrow { get; set; }

        /// <summary>
        ///     Whether or not this Game is completed, and should not have any more throws.
        /// </summary>
        public bool IsFinished { get; set; }
    }
}