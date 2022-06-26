using System.Data.Entity;

namespace ChessLibrary
{
    public class PlayerContext : DbContext
    {
        public PlayerContext()
            : base(@"data source=(localdb)\MSSQLLocalDB;Initial Catalog=ChessStore;Integrated Security=True;")
        { }

        public DbSet<Player> Players { get; set; }
        public DbSet<GamesStats> GamesStats { get; set; }
    }
}