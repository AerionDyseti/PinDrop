using Microsoft.EntityFrameworkCore;
using PinDrop.Models.DataModels;

namespace PinDrop
{
    /// <summary>
    ///     EntityFramework Database Context, which serves as Repository for database.
    /// </summary>
    public class PinDropContext : DbContext
    {
        public PinDropContext(DbContextOptions<PinDropContext> options) : base(options)
        {
        }

        public DbSet<PlayerDataModel> Players { get; set; }
        public DbSet<FrameDataModel> Frames { get; set; }
        public DbSet<GameDataModel> Games { get; set; }
    }
}