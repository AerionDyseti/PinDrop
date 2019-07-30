using System;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    /// DTO for displaying Players Data.
    /// Using a DTO rather than outputting the DataModel allows
    /// control over displaying sensitive properties, or computation of properties
    /// that display differently than they are stored in the DB.
    /// </summary>
    public class PlayerViewModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}
