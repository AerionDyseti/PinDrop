using System;

namespace PinDrop.Models.DataModels
{
    /// <summary>
    ///     Data model representing a single Frame of bowling.
    /// </summary>
    public class FrameDataModel
    {
        /// <summary>
        ///     Unique ID for this Frame.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The Unique ID of the Game this Frame belongs to.
        /// </summary>

        public Guid GameId { get; set; }

        /// <summary>
        ///     Which frame this is inside a game.
        /// </summary>
        public int FrameNumber { get; set; }

        /// <summary>
        ///     The number of pins dropped on the first throw.
        /// </summary>
        public int? FirstThrow { get; set; }

        /// <summary>
        ///     The number of pins dropped on the second throw.
        /// </summary>
        public int? SecondThrow { get; set; }

        /// <summary>
        ///     The number of pins dropped on the third throw.
        /// </summary>
        public int? ThirdThrow { get; set; }

        /// <summary>
        ///     Whether or not this Frame is a Strike.
        /// </summary>
        public bool IsStrike { get; set; }

        /// <summary>
        ///     Whether or not this frame is a Spare.
        /// </summary>
        public bool IsSpare { get; set; }

        /// <summary>
        ///     Whether or not this frame is completed and should not have anymore throws.
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        ///     The Date/Time this Frame was created.
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}