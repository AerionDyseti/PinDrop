using System;
using System.Collections.Generic;

namespace PinDrop.Models.RequestModels
{
    /// <summary>
    /// A DTO for creating a new game.
    /// </summary>
    public class CreateGameRequest
    {
        public List<Guid> PlayerIds { get; set; }
    }
}
