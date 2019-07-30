using System;

namespace PinDrop.Models.RequestModels
{
    /// <summary>
    /// DTO used to add a player's throw to a game.
    /// </summary>
    public class CreateThrowRequest
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int PinsDropped { get; set; }
    }
}
