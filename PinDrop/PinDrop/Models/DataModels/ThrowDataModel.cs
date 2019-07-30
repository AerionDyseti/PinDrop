using System;

namespace PinDrop.Models.DataModels
{

    /// <summary>
    /// Represents a throw for a single player inside a single Frame.
    /// </summary>
    public class ThrowDataModel
    {

        /// <summary>
        /// Unique ID for the Game this Throw was in.
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// Unique ID for the Players that made this Throw.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 1-based integer representing the number for the frame this throw was in.
        /// </summary>
        public int FrameNumber { get; set; }

        /// <summary>
        /// 1-based integer representing which throw (inside the frame) this throw is.
        /// </summary>
        public int ThrowNumber { get; set; }

        /// <summary>
        /// The number of pins that were dropped by this throw.
        /// </summary>
        public int PinsDropped { get; set; }

        /// <summary>
        /// Date/Time this throw was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

    }

}
