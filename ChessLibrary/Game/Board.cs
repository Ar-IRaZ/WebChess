using System.Collections.Generic;

namespace ChessLibrary.Game
{
    public class Board
    {
        public bool IsWhiteCheck { get; set; }
        public bool IsBlackCheck { get; set; }
        public Square[,] Squares { get; set; }
        public int[] CursoreSelected { get; set; }
        public List<Square[]> BlackCheckVector { get; set; }
        public List<Square[]> WhiteCheckVector { get; set; }
        public byte IsMoveUnderWhiteCheck { get; set; }
        public byte IsMoveUnderBlackCheck { get; set; }

        public void UpdateBoard()
        {

        }

        public Board CreateBoard(Fen fen)
        {
            Squares = new Square[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Squares[i, j] = new Square(fen.Figures[i, j], i, j);
                }
            }
            Squares[6, 3].CursoreSelected = true;

            IsMoveUnderWhiteCheck = 0;
            IsMoveUnderBlackCheck = 0;
            CursoreSelected = new int[] { 6, 3 };
            BlackCheckVector = new List<Square[]>();
            WhiteCheckVector = new List<Square[]>();
            return this;
        }

        internal Board Clone()
        {
            Board board = new Board();

            board.Squares = new Square[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board.Squares[i, j] = new Square(Squares[i, j].Figure, i, j);
                }
            board.BlackCheckVector = new List<Square[]>();
            board.WhiteCheckVector = new List<Square[]>();
            return board;

        }
    }
}