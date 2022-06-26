using System.Data.Entity;

namespace ChessLibrary.Models
{
    public class GameContext : DbContext
    {

        public GameContext()
            : base(@"data source=(localdb)\MSSQLLocalDB;Initial Catalog=ChessStore;Integrated Security=True;")
        { }

        public DbSet<GameModel> Game { get; set; }
    }
}
