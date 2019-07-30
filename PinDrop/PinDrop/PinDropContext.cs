using Microsoft.EntityFrameworkCore;
using PinDrop.Models.DataModels;

namespace PinDrop
{
    /// <summary>
    /// EntityFramework Database Context, which serves as Repository for database.
    /// </summary>
    public class PinDropContext : DbContext
    {
        public PinDropContext (DbContextOptions<PinDropContext> options) : base(options)
        {
        }

        public DbSet<PlayerDataModel> Players { get; set; }

        public DbSet<ThrowDataModel> Throws { get; set; }

        public DbSet<GameDataModel> Games { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key made up of throw number, frame number, player throwing, and game thrown in.
            modelBuilder.Entity<ThrowDataModel>()
                .HasKey(t => new {t.GameId, t.PlayerId, t.FrameNumber, t.ThrowNumber});

            // Do not allow duplicate names. 
            modelBuilder.Entity<PlayerDataModel>()
                .HasIndex(p => p.Name).IsUnique();
        }

    }

}
