namespace PinDrop.Models.RequestModels
{
    /// <summary>
    ///     DTO used to add a player's throw to a game.
    /// </summary>
    public class CreateThrowRequest
    {
        public int PinsDropped { get; set; }
    }
}