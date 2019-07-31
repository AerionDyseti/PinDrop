using System;

namespace PinDrop.Models.RequestModels
{
    /// <summary>
    ///     A DTO for creating a new game.
    /// </summary>
    public class CreateGameRequest
    {
        public Guid PlayerId { get; set; }
    }
}