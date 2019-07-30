using System;

namespace PinDrop.Models.DataModels
{
    /// <summary>
    /// Represents a single Players.
    /// </summary>
    public class PlayerDataModel
    {
       
        /// <summary>
        /// Unique ID for this Players.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// This player's displayed name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Date/Time this player was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

    }
}
