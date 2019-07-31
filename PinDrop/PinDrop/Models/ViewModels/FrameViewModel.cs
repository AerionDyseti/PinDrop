using System.Collections.Generic;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    ///     DTO for displaying a frame inside a game of bowling.
    /// </summary>
    public class FrameViewModel
    {
        /// <summary>
        ///     The 1-based frame number for this throw.
        /// </summary>
        public int FrameNumber { get; set; }

        /// <summary>
        ///     The string representation of the throws for this frame.
        /// </summary>
        public List<char> Throws { get; set; }

        /// <summary>
        ///     The score for this specific frame.
        /// </summary>
        public int? Score { get; set; }
    }
}