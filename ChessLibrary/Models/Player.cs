namespace ChessLibrary
{
    public class Player
    {

        public int PlayerId { get; set; }
        public string NickName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual GamesStats GamesStats { get; set; }

    }
}