using System;

namespace PinDrop.Models.RequestModels
{
    /// <summary>
    /// DTO to facilitate creating a new player.
    /// </summary>
    public class CreatePlayerRequest
    {
        public String Name { get; set; }
    }
}
