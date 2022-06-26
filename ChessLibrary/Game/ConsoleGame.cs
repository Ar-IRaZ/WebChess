using ChessLibrary;
using System;
using System.Collections.Generic;

namespace ChessLibrary.Game
{
    public class ConsoleGame// :GamePart
    {
        private int status;      //0 - Game continue, <0 - Game ended

        public Fen Fen { get; private set; }
        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }
        private Board Board { get; set; }
        private bool IsSelectedForMove { get; set; }
        private Square SelectedForMove { get; set; }


        public ConsoleGame(string fen, Player whitePlayer, Player blackPlayer)
        {
            status = 0;
            Fen = new Fen(fen);
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            IsSelectedForMove = false;
            Board = new Board().CreateBoard(Fen);
            //Update();
        }     
        //public override void Update()
        //{
        //    foreach (Square sq in Board.Squares)
        //    {
        //        if (sq.FiguresCanMove.Count != 0)
        //            sq.FiguresCanMove = new List<Square>();
        //    }

        //    Square whiteKing = null, blackKing = null;
        //    foreach (Square sq in Board.Squares)
        //    {
        //        switch (sq.Figure)
        //        {
        //            case Figure.blackKing:
        //                blackKing = sq;
        //                break;
        //            case Figure.whiteKing:
        //                whiteKing = sq;
        //                break;
        //            default:
        //                ChessRools.GetPossibleMove(sq, Board);
        //                break;
        //        }
        //    }

        //    if (Fen.ActiveSide == Color.black)
        //    {
        //        try
        //        {
        //            ChessRools.KingBaseMove(whiteKing, Board);
        //            ChessRools.GetPossibleMove(blackKing, Board);
        //        }
        //        catch (NullReferenceException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }
        //    else
        //    {
        //        try
        //        {
        //            ChessRools.KingBaseMove(blackKing, Board);
        //            ChessRools.GetPossibleMove(whiteKing, Board);
        //        }
        //        catch (NullReferenceException e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //    if (Board.IsWhiteCheck || Board.IsBlackCheck)
        //    {
        //        Board.WhiteCheckVector.Clear();
        //        Board.BlackCheckVector.Clear();
        //        foreach (Square sq in Board.Squares)
        //        {
        //            if (sq.FiguresCanMove.Count != 0)
        //                sq.FiguresCanMove = new List<Square>();
        //        }

        //        whiteKing = null; blackKing = null;
        //        foreach (Square sq in Board.Squares)
        //        {
        //            switch (sq.Figure)
        //            {
        //                case Figure.blackKing:
        //                    blackKing = sq;
        //                    break;
        //                case Figure.whiteKing:
        //                    whiteKing = sq;
        //                    break;
        //                default:
        //                    ChessRools.GetPossibleMove(sq, Board);
        //                    break;
        //            }
        //        }

        //        if (Fen.ActiveSide == Color.black)
        //        {
        //            try
        //            {
        //                ChessRools.KingBaseMove(whiteKing, Board);
        //                ChessRools.GetPossibleMove(blackKing, Board);
        //            }
        //            catch (NullReferenceException e)
        //            {
        //                Console.WriteLine(e.Message);
        //            }

        //        }
        //        else
        //        {
        //            try
        //            {
        //                ChessRools.KingBaseMove(blackKing, Board);
        //                ChessRools.GetPossibleMove(whiteKing, Board);
        //            }
        //            catch (NullReferenceException e)
        //            {
        //                Console.WriteLine(e.Message);
        //            }
        //        }
        //    }
        //    if (Board.IsMoveUnderBlackCheck == 2 || Board.IsMoveUnderWhiteCheck == 2)
        //        status = 2;
        //}

        public static void Update(Board Board, Color ActiveSide)
        {
            foreach (Square sq in Board.Squares)
            {
                if (sq.FiguresCanMove.Count != 0)
                    sq.FiguresCanMove = new List<Square>();
            }

            Square whiteKing = null, blackKing = null;
            foreach (Square sq in Board.Squares)
            {
                switch (sq.Figure)
                {
                    case Figure.blackKing:
                        blackKing = sq;
                        break;
                    case Figure.whiteKing:
                        whiteKing = sq;
                        break;
                    default:
                        ChessRools.GetPossibleMove(sq, Board);
                        break;
                }
            }
            if (ActiveSide == Color.black)
            {
                try
                {
                    ChessRools.KingMoveEnded(whiteKing, Board);
                    ChessRools.KingUnderAttack(blackKing, Board);

                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                try
                {
                    ChessRools.KingMoveEnded(blackKing, Board);
                    ChessRools.KingUnderAttack(whiteKing, Board);

                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}