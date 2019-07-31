using System;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    ///     DTO for displaying Players Data.
    ///     Using a DTO rather than outputting the DataModel allows
    ///     control over displaying sensitive properties, or computation of properties
    ///     that display differently than they are stored in the DB.
    /// </summary>
    public class PlayerViewModel
    {
        /// <summary>
        ///     The unique Identifier for this Player.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     This Player's displayed name.
        /// </summary>
        public String Name { get; set; }
    }
}