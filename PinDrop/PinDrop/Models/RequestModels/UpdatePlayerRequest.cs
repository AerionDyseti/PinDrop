using System;

namespace PinDrop.Models.RequestModels
{
    /// <summary>
    /// DTO for requesting the update of a player.
    /// Only includes properties that are allowed to be updated.
    /// </summary>
    public class UpdatePlayerRequest
    {
        public String Name { get; set; }
    }

}
