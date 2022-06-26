using System.Text;

namespace ChessLibrary.Game
{
    public class Fen
    {
        public Figure[,] Figures { get; set; }
        public Color ActiveSide { get; set; }
        public bool[] Castling { get; set; }
        public Square Passant { get; set; }
        public int MoveCount { get; set; }
        public int HalfMoveCount { get; set; }


        public Fen(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            ParseFen(fen);
        }

        public void ParseFen(string fen)
        {
            string[] splitedByEmpty = fen.Split(' ');
            string[] splitedByLine = splitedByEmpty[0].Split('/');
            Figures = new Figure[8, 8];
            int f, l;
            for (int i = 0; i < 8; i++)
            {
                l = 0;
                for (int j = 0; j < splitedByLine[i].Length; j++)
                {
                    if (int.TryParse(new string(splitedByLine[i][j], 1), out f))
                    {
                        for (int k = f; k > 0; k--)
                        {
                            Figures[i, l + k - 1] = Figure.none;
                        }
                        l += f - 1;
                    }
                    else
                    {
                        Figures[i, l] = (Figure)splitedByLine[i][j];
                    }
                    l++;
                }
            }


            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        Console.Write((char)Figures[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            ActiveSide = (Color)splitedByEmpty[1][0];

            Castling = new bool[4] { false, false, false, false };
            foreach (char ch in splitedByEmpty[2])
                switch (ch)
                {
                    case 'K':
                        Castling[0] = true;
                        break;
                    case 'Q':
                        Castling[1] = true;
                        break;
                    case 'k':
                        Castling[2] = true;
                        break;
                    case 'q':
                        Castling[3] = true;
                        break;
                }

            MoveCount = int.Parse(splitedByEmpty[4]);
            HalfMoveCount = int.Parse(splitedByEmpty[5]);
        }
        public void PasreFen()
        {

        }

        public void ResetBoard(Square[,] sq)
        {
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    Figures[i, j] = sq[i, j].Figure;
                }
            // Console.ReadKey(true);
        }

        public void UpdateFen(Board board)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Figures[i, j] = board.Squares[i, j].Figure;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            int n = 0;
            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    if (Figures[i, j] != Figure.none)
                    {
                        if (n == 0)
                        {
                            builder.Append(((char)Figures[i, j]));
                        }
                        else
                        {

                            builder.Append(n.ToString());
                            builder.Append(((char)Figures[i, j]));
                            n = 0;
                        }
                    }
                    else
                    {
                        n++;
                    }

                }
                if (n != 0)
                {
                    builder.Append(n.ToString());
                }
                n = 0;
                if (i != 7)
                    builder.Append('/');
            }

            builder.Append(' ');
            builder.Append(((char)ActiveSide));
            builder.Append(' ');

            if (Castling[0])
                builder.Append('K');
            if (Castling[1])
                builder.Append('Q');
            if (Castling[2])
                builder.Append('k');
            if (Castling[3])
                builder.Append('q');

            builder.Append(" - ");
            builder.Append(MoveCount.ToString());
            builder.Append(new string(' ' + HalfMoveCount.ToString()));

            return builder.ToString();
        }
    }


}

