using System.ComponentModel.DataAnnotations.Schema;

namespace ChessLibrary
{
    public class GamesStats
    {
        public GamesStats()
        {
            CountOfGame = 0;
            CountWhiteSideGame = 0;
            CountBlackSideGame = 0;
            CountVictory = 0;
            CountWhiteSideVictory = 0;
            CountBlackSideVictory = 0;
        }

        [ForeignKey("Player")]
        public int GamesStatsId { get; set; }
        public virtual Player Player { get; set; }
        public int CountOfGame { get; set; }
        public int CountWhiteSideGame { get; set; }
        public int CountBlackSideGame { get; set; }
        public int CountVictory { get; set; }
        public int CountWhiteSideVictory { get; set; }
        public int CountBlackSideVictory { get; set; }
    }
}