using System.Collections.Generic;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    /// DTO for displaying the score of a single player within a game.
    /// </summary>
    public class GameScoreViewModel
    {
        /// <summary>
        /// The Player this score is for.
        /// </summary>
        public PlayerViewModel Player { get; set; }

        /// <summary>
        /// A list of the scores in this game.
        /// </summary>
        public List<FrameViewModel> Frames { get; set; }

        /// <summary>
        /// Total score for this player.
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// The 1-based index of the frame this player is on.
        /// </summary>
        public int CurrentFrame { get; set; }

        /// <summary>
        /// The 1-based index of the throw this player is on.
        /// </summary>
        public int CurrentThrow { get; set; }

    }

}
