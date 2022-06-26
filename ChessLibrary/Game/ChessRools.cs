using ChessLibrary.Game;
using System;
using System.Collections.Generic;

namespace ChessLibrary
{
    public static class ChessRools
    {
        public static List<Figure> whiteFigures = new List<Figure>() { Figure.whiteKing, Figure.whiteBishop, Figure.whiteKnight, Figure.whitePawn, Figure.whiteQueen, Figure.whiteRook };
        public static List<Figure> blackFigures = new List<Figure>() { Figure.blackKing, Figure.blackBishop, Figure.blackKnight, Figure.blackPawn, Figure.blackQueen, Figure.blackRook };

        public static void GetPossibleMove(Square sq, Board board)
        {

            switch (sq.Figure)
            {
                case Figure.blackPawn:
                    PawnMove(sq, board);
                    break;

                case Figure.whitePawn:
                    PawnMove(sq, board);
                    break;

                case Figure.whiteQueen:
                    QueenMove(sq, board);
                    break;

                case Figure.blackQueen:
                    QueenMove(sq, board);
                    break;

                case Figure.blackKnight:
                    KnightMove(sq, board);
                    break;
                case Figure.whiteKnight:
                    KnightMove(sq, board);
                    break;

                case Figure.whiteBishop:
                    BishopMove(sq, board);
                    break;

                case Figure.blackBishop:
                    BishopMove(sq, board);
                    break;

                case Figure.whiteRook:
                    RookMove(sq, board);
                    break;

                case Figure.blackRook:
                    RookMove(sq, board);
                    break;

                case Figure.whiteKing:
                    KingMove(sq, board);
                    break;

                case Figure.blackKing:
                    KingMove(sq, board);
                    break;

                default:
                    break;
            }

        }

        private static void RookMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;
            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);
            int i;
            if (board.Squares[y, x].Figure == Figure.whiteRook)
            {
                if (!board.IsWhiteCheck)
                {
                    #region Down
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (blackFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Up
                    for (i = y - 1; i >= 0; i--)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {

                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (blackFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Right
                    for (i = x + 1; i < 8; i++)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {

                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (blackFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Left
                    for (i = x - 1; i >= 0; i--)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {

                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (blackFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                }
                else
                {
                    if (board.WhiteCheckVector.Count == 1)
                        foreach (Square[] vect in board.WhiteCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            #region Down
                            for (i = y + 1; i < 8; i++)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Up
                            for (i = y - 1; i >= 0; i--)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Right
                            for (i = x + 1; i < 8; i++)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Left
                            for (i = x - 1; i >= 0; i--)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderWhiteCheck = 1;
                }
            }
            else
            {
                if (!board.IsBlackCheck)
                {

                    #region Down
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Up
                    for (i = y - 1; i >= 0; i--)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Right
                    for (i = x + 1; i < 8; i++)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Left
                    for (i = x - 1; i >= 0; i--)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                }
                else
                {
                    if (board.BlackCheckVector.Count == 1)
                        foreach (Square[] vect in board.BlackCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            #region Down
                            for (i = y + 1; i < 8; i++)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Up
                            for (i = y - 1; i >= 0; i--)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Right
                            for (i = x + 1; i < 8; i++)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Left
                            for (i = x - 1; i >= 0; i--)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderBlackCheck = 1;
                }
            }
        }

        private static void BishopMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;
            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);
            if (board.Squares[y, x].Figure == Figure.whiteBishop)
            {
                if (!board.IsWhiteCheck)
                {
                    #region RightDown
                    int i = y + 1;
                    int j = x + 1;
                    while (j < 8 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i++;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region RightUp
                    i = y - 1;
                    j = x + 1;
                    while (j < 8 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i--;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftUp
                    i = y - 1;
                    j = x - 1;
                    while (j >= 0 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i--;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftDown
                    i = y + 1;
                    j = x - 1;

                    while (j >= 0 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i++;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    #endregion
                }
                else
                {
                    if (board.WhiteCheckVector.Count == 1)
                        foreach (Square[] vect in board.WhiteCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            #region RightDown
                            int i = y + 1;
                            int j = x + 1;
                            while (j < 8 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i++;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region RightUp
                            i = y - 1;
                            j = x + 1;
                            while (j < 8 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i--;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftUp
                            i = y - 1;
                            j = x - 1;
                            while (j >= 0 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i--;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftDown
                            i = y + 1;
                            j = x - 1;

                            while (j >= 0 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i++;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            #endregion

                            if (board.Squares[y, x].FiguresCanMove.Count > 1)
                                board.IsMoveUnderWhiteCheck = 1;
                        }
                }
            }
            else
            {
                if (!board.IsBlackCheck)
                {
                    #region RightDown
                    int i = y + 1;
                    int j = x + 1;
                    while (j < 8 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i++;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region RightUp
                    i = y - 1;
                    j = x + 1;
                    while (j < 8 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i--;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftUp
                    i = y - 1;
                    j = x - 1;
                    while (j >= 0 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i--;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftDown
                    i = y + 1;
                    j = x - 1;

                    while (j >= 0 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i++;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    #endregion
                }
                else
                {
                    if (board.BlackCheckVector.Count == 1)
                        foreach (Square[] vect in board.BlackCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            #region RightDown
                            int i = y + 1;
                            int j = x + 1;
                            while (j < 8 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i++;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region RightUp
                            i = y - 1;
                            j = x + 1;
                            while (j < 8 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i--;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftUp
                            i = y - 1;
                            j = x - 1;
                            while (j >= 0 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i--;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftDown
                            i = y + 1;
                            j = x - 1;

                            while (j >= 0 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i++;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            #endregion
                        }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderBlackCheck = 1;

                }
            }
        }

        private static void QueenMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;
            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);

            if (board.Squares[y, x].Figure == Figure.whiteQueen)
            {
                if (!board.IsWhiteCheck)
                {
                    #region RightDown
                    int i = y + 1;
                    int j = x + 1;
                    while (j < 8 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i++;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region RightUp
                    i = y - 1;
                    j = x + 1;
                    while (j < 8 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i--;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftUp
                    i = y - 1;
                    j = x - 1;
                    while (j >= 0 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i--;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftDown
                    i = y + 1;
                    j = x - 1;

                    while (j >= 0 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i++;
                        }
                        else if (blackFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    #endregion

                    #region Down
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (blackFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.IsBlackCheck = true;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Up
                    for (i = y - 1; i >= 0; i--)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (blackFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Right
                    for (i = x + 1; i < 8; i++)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (blackFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Left
                    for (i = x - 1; i >= 0; i--)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (blackFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsBlackCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                }
                else
                {
                    if (board.WhiteCheckVector.Count == 1)
                        foreach (Square[] vect in board.WhiteCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            #region RightDown
                            int i = y + 1;
                            int j = x + 1;
                            while (j < 8 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i++;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region RightUp
                            i = y - 1;
                            j = x + 1;
                            while (j < 8 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i--;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftUp
                            i = y - 1;
                            j = x - 1;
                            while (j >= 0 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i--;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftDown
                            i = y + 1;
                            j = x - 1;

                            while (j >= 0 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i++;
                                }
                                else if (blackFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Down
                            for (i = y + 1; i < 8; i++)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Up
                            for (i = y - 1; i >= 0; i--)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Right
                            for (i = x + 1; i < 8; i++)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Left
                            for (i = x - 1; i >= 0; i--)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (blackFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderWhiteCheck = 1;
                }
            }
            if (board.Squares[y, x].Figure == Figure.blackQueen)
            {
                if (!board.IsBlackCheck)
                {
                    #region RightDown
                    int i = y + 1;
                    int j = x + 1;
                    while (j < 8 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i++;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region RightUp
                    i = y - 1;
                    j = x + 1;
                    while (j < 8 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j++;
                            i--;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftUp
                    i = y - 1;
                    j = x - 1;
                    while (j >= 0 && i >= 0)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i--;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region LeftDown
                    i = y + 1;
                    j = x - 1;

                    while (j >= 0 && i < 8)
                    {
                        if (board.Squares[i, j].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            j--;
                            i++;
                        }
                        else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                            if (board.Squares[i, j].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, j] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    #endregion

                    #region Down
                    for (i = y + 1; i < 8; i++)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Up
                    for (i = y - 1; i >= 0; i--)
                    {
                        if (board.Squares[i, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                        }
                        else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                            if (board.Squares[i, x].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[i, x] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Right
                    for (i = x + 1; i < 8; i++)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    #region Left
                    for (i = x - 1; i >= 0; i--)
                    {
                        if (board.Squares[y, i].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                        }
                        else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                            if (board.Squares[y, i].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y, i] });
                                board.IsWhiteCheck = true;
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    if (board.BlackCheckVector.Count == 1)
                        foreach (Square[] vect in board.BlackCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            #region RightDown
                            int i = y + 1;
                            int j = x + 1;
                            while (j < 8 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i++;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region RightUp
                            i = y - 1;
                            j = x + 1;
                            while (j < 8 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j++;
                                    i--;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftUp
                            i = y - 1;
                            j = x - 1;
                            while (j >= 0 && i >= 0)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i--;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {

                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region LeftDown
                            i = y + 1;
                            j = x - 1;

                            while (j >= 0 && i < 8)
                            {
                                int result = j * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - j, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - j, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, j].Figure == Figure.none)
                                {

                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    j--;
                                    i++;
                                }
                                else if (whiteFigures.Contains(board.Squares[i, j].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, j]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            #endregion

                            #region Down
                            for (i = y + 1; i < 8; i++)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Up
                            for (i = y - 1; i >= 0; i--)
                            {
                                int result = x * (yK - yAt) - i * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - i, 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - i, 2)));
                                if (board.Squares[i, x].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[i, x].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[i, x]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Right
                            for (i = x + 1; i < 8; i++)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion

                            #region Left
                            for (i = x - 1; i >= 0; i--)
                            {
                                int result = i * (yK - yAt) - y * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - i, 2) + Math.Pow(yAt - y, 2))) - Math.Sqrt((Math.Pow(xK - i, 2) + Math.Pow(yK - y, 2)));
                                if (board.Squares[y, i].Figure == Figure.none)
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                }
                                else if (whiteFigures.Contains(board.Squares[y, i].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderBlackCheck = 1;
                }
            }
        }

        private static void KnightMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;
            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);
            if (board.Squares[y, x].Figure == Figure.whiteKnight)
            {
                if (!board.IsWhiteCheck)
                {
                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y - 2, x - 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x - 1]);
                            if (board.Squares[y - 2, x - 1].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 2, x - 1] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//UpLeft

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y - 2, x + 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x + 1]);
                            if (board.Squares[y - 2, x + 1].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 2, x + 1] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//UpRight

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y + 2, x + 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x + 1]);
                            if (board.Squares[y + 2, x + 1].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 2, x + 1] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//DownRight

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y + 2, x - 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x - 1]);
                            if (board.Squares[y + 2, x - 1].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 2, x - 1] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//DownLeft

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y - 1, x - 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 2]);
                            if (board.Squares[y - 1, x - 2].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x - 2] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//LeftUp

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y - 1, x + 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 2]);
                            if (board.Squares[y - 1, x + 2].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x + 2] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//RightUp

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y + 1, x + 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 2]);
                            if (board.Squares[y + 1, x + 2].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x + 2] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//RightDown

                    try
                    {
                        if (!whiteFigures.Contains(board.Squares[y + 1, x - 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 2]);
                            if (board.Squares[y + 1, x - 2].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x - 2] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//LeftDown
                }
                else
                {
                    if (board.WhiteCheckVector.Count == 1)
                        foreach (Square[] vect in board.WhiteCheckVector)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            try
                            {
                                int result = (x - 1) * (yK - yAt) - (y - 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 1), 2) + Math.Pow(yAt - (y - 2), 2))) - Math.Sqrt((Math.Pow(xK - (x - 1), 2) + Math.Pow(yK - (y - 2), 2)));
                                if (!whiteFigures.Contains(board.Squares[y - 2, x - 1].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x - 1]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//UpLeft

                            try
                            {
                                int result = (x + 1) * (yK - yAt) - (y - 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 1), 2) + Math.Pow(yAt - (y - 2), 2))) - Math.Sqrt((Math.Pow(xK - (x + 1), 2) + Math.Pow(yK - (y - 2), 2)));
                                if (!whiteFigures.Contains(board.Squares[y - 2, x + 1].Figure))
                                {
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x + 1]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//UpRight

                            try
                            {
                                if (!whiteFigures.Contains(board.Squares[y + 2, x + 1].Figure))
                                {
                                    int result = (x + 1) * (yK - yAt) - (y + 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 1), 2) + Math.Pow(yAt - (y + 2), 2))) - Math.Sqrt((Math.Pow(xK - (x + 1), 2) + Math.Pow(yK - (y + 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x + 1]);
                                    }

                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//DownRight

                            try
                            {
                                if (!whiteFigures.Contains(board.Squares[y + 2, x - 1].Figure))
                                {
                                    int result = (x - 1) * (yK - yAt) - (y + 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 1), 2) + Math.Pow(yAt - (y + 2), 2))) - Math.Sqrt((Math.Pow(xK - (x - 1), 2) + Math.Pow(yK - (y + 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x - 1]);
                                    }

                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//DownLeft

                            try
                            {
                                if (!whiteFigures.Contains(board.Squares[y - 1, x - 2].Figure))
                                {
                                    int result = (x - 2) * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 2), 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - (x - 2), 2) + Math.Pow(yK - (y - 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//LeftUp

                            try
                            {
                                if (!whiteFigures.Contains(board.Squares[y - 1, x + 2].Figure))
                                {
                                    int result = (x + 2) * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 2), 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - (x + 2), 2) + Math.Pow(yK - (y - 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//RightUp

                            try
                            {
                                if (!whiteFigures.Contains(board.Squares[y + 1, x + 2].Figure))
                                {
                                    int result = (x + 2) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 2), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x + 2), 2) + Math.Pow(yK - (y + 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//RightDown

                            try
                            {
                                if (!whiteFigures.Contains(board.Squares[y + 1, x - 2].Figure))
                                {
                                    int result = (x - 2) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 2), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x - 2), 2) + Math.Pow(yK - (y + 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//LeftDown
                        }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderWhiteCheck = 1;
                }
            }
            else
            {
                if (!board.IsBlackCheck)
                {
                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y - 2, x - 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x - 1]);
                            if (board.Squares[y - 2, x - 1].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 2, x - 1] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//DownRight+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y - 2, x + 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x + 1]);
                            if (board.Squares[y - 2, x + 1].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 2, x + 1] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//DownLeft+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y + 2, x + 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x + 1]);
                            if (board.Squares[y + 2, x + 1].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 2, x + 1] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//UpLeft+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y + 2, x - 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x - 1]);
                            if (board.Squares[y + 2, x - 1].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 2, x - 1] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//UpRight+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y - 1, x - 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 2]);
                            if (board.Squares[y - 1, x - 2].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x - 2] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//RightDown+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y - 1, x + 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 2]);
                            if (board.Squares[y - 1, x + 2].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x + 2] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//LeftDown+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y + 1, x + 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 2]);
                            if (board.Squares[y + 1, x + 2].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x + 2] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//LeftUp+

                    try
                    {
                        if (!blackFigures.Contains(board.Squares[y + 1, x - 2].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 2]);
                            if (board.Squares[y + 1, x - 2].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x - 2] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//RightUp+
                }
                else
                {
                    foreach (Square[] vect in board.BlackCheckVector)
                    {
                        if (board.BlackCheckVector.Count == 1)
                        {
                            #region Coordinates
                            int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                            #endregion

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y - 2, x - 1].Figure))
                                {
                                    int result = (x - 1) * (yK - yAt) - (y - 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 1), 2) + Math.Pow(yAt - (y - 2), 2))) - Math.Sqrt((Math.Pow(xK - (x - 1), 2) + Math.Pow(yK - (y - 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x - 1]);
                                    }

                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//DownRight+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y - 2, x + 1].Figure))
                                {
                                    int result = (x + 1) * (yK - yAt) - (y - 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 1), 2) + Math.Pow(yAt - (y - 2), 2))) - Math.Sqrt((Math.Pow(xK - (x + 1), 2) + Math.Pow(yK - (y - 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x + 1]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//DownLeft+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y + 2, x + 1].Figure))
                                {
                                    int result = (x + 1) * (yK - yAt) - (y + 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 1), 2) + Math.Pow(yAt - (y + 2), 2))) - Math.Sqrt((Math.Pow(xK - (x + 1), 2) + Math.Pow(yK - (y + 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x + 1]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//UpLeft+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y + 2, x - 1].Figure))
                                {
                                    int result = (x - 1) * (yK - yAt) - (y + 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 1), 2) + Math.Pow(yAt - (y + 2), 2))) - Math.Sqrt((Math.Pow(xK - (x - 1), 2) + Math.Pow(yK - (y + 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x - 1]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//UpRight+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y - 1, x - 2].Figure))
                                {
                                    int result = (x - 2) * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 2), 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - (x - 2), 2) + Math.Pow(yK - (y - 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//RightDown+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y - 1, x + 2].Figure))
                                {
                                    int result = (x + 2) * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 2), 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - (x + 2), 2) + Math.Pow(yK - (y - 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//LeftDown+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y + 1, x + 2].Figure))
                                {
                                    int result = (x + 2) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 2), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x + 2), 2) + Math.Pow(yK - (y + 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//LeftUp+

                            try
                            {
                                if (!blackFigures.Contains(board.Squares[y + 1, x - 2].Figure))
                                {
                                    int result = (x - 2) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 2), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x - 2), 2) + Math.Pow(yK - (y + 1), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 2]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//RightUp+
                        }
                    }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderBlackCheck = 1;
                }
            }
        }

        public static void KingMoveEnded(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;

            if (board.Squares[y, x].Figure == Figure.whiteKing)
            {

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y - 1, x]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;0

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x - 1].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y - 1, x - 1]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;-1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y, x - 1].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y, x - 1]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;-1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y + 1, x]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;0

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x + 1].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y + 1, x + 1]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;+1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y, x + 1].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (blackFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y, x + 1]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;+1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x + 1].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (blackFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x + 1]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;+1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x - 1].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (blackFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x - 1]))
                                {
                                    k = true;
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;-1   
            }
            else
            {
                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;0

                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x - 1]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;-1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y, x - 1]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;-1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;0

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x + 1]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;+1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y, x + 1]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;+1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x + 1]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;+1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x - 1]))
                                {
                                    k = true;
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;-1                
            }
        }
        public static void KingBaseMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;

            if (board.Squares[y, x].Figure == Figure.whiteKing)
            {

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y - 1, x]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;0

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x - 1].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y - 1, x - 1]))
                                {
                                    k = true;

                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;-1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y, x - 1].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y, x - 1]))
                                {
                                    k = true;

                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;-1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y + 1, x]))
                                {
                                    k = true;

                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;0

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x + 1].Figure))
                    {
                        bool k = false;
                        foreach (Square squa in board.Squares)
                        {
                            if (blackFigures.Contains(squa.Figure))
                            {
                                if (squa.FiguresCanMove.Contains(board.Squares[y + 1, x + 1]))
                                {
                                    k = true;

                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;+1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y, x + 1].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (blackFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y, x + 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;+1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x + 1].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (blackFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x + 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;+1

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x - 1].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (blackFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x - 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;-1   
            }
            else
            {
                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x].Figure))
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;0

                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x - 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;-1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y, x - 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;-1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;0

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x + 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;+1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y, x + 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;+1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y - 1, x + 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;+1

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        bool k = false;
                        foreach (Square s in board.Squares)
                        {
                            if (whiteFigures.Contains(s.Figure))
                            {
                                if (s.FiguresCanMove.Contains(board.Squares[y + 1, x - 1]))
                                {
                                    k = true;
                                }
                            }
                        }
                        if (!k)
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;-1                
            }
        }
        public static void KingUnderAttack(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;

            if (board.Squares[y, x].Figure == Figure.whiteKing)
            {
                foreach (Square s in board.Squares)
                {
                    if (blackFigures.Contains(s.Figure))
                    {
                        if (s.FiguresCanMove.Contains(sq))
                        {
                            board.IsWhiteCheck = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Square s in board.Squares)
                {
                    if (whiteFigures.Contains(s.Figure))
                    {
                        if (s.FiguresCanMove.Contains(sq))
                        {
                            board.IsBlackCheck = true;
                        }
                    }
                }

            }
        }
        private static void KingMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;

            if (board.Squares[y, x].Figure == Figure.whiteKing)
            {

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x].Figure) /*&& board.Squares[y - 1, x].Figure!=Figure.blackKing*/)
                    {

                        Board b = board.Clone();

                        b.Squares[y - 1, x].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);
                        }

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;0+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y - 1, x - 1].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;-1+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y, x - 1].Figure)/* && board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y, x - 1].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x - 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;-1+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y + 1, x].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;0+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();
                        b.Squares[y + 1, x + 1].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;+1+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();
                        b.Squares[y, x + 1].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;+1+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y - 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();
                        b.Squares[y - 1, x + 1].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;+1+

                try
                {
                    if (!whiteFigures.Contains(board.Squares[y + 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.blackKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y + 1, x - 1].Figure = Figure.whiteKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.white);

                        if (!b.IsWhiteCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;-1


                board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);
                if (board.Squares[y, x].FiguresCanMove.Count > 1)
                    board.IsMoveUnderWhiteCheck = 1;

            }
            else
            {
                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {

                        Board b = board.Clone();

                        b.Squares[y - 1, x].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);
                        }

                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;0+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y - 1, x - 1].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;-1+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y, x - 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y, x - 1].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x - 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;-1+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y + 1, x].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;0+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x + 1].Figure) /*&& board.Squares[y - 1, x].Figure != Figure.whiteKing*/)
                    {
                        Board b = board.Clone();

                        b.Squares[y + 1, x + 1].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;+1+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y, x + 1].Figure))
                    {
                        Board b = board.Clone();

                        b.Squares[y, x + 1].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//0;+1+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y - 1, x + 1].Figure))
                    {
                        Board b = board.Clone();

                        b.Squares[y, x + 1].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x + 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//-1;+1+

                try
                {
                    if (!blackFigures.Contains(board.Squares[y + 1, x - 1].Figure))
                    {
                        Board b = board.Clone();

                        b.Squares[y + 1, x - 1].Figure = Figure.blackKing;
                        b.Squares[y, x].Figure = Figure.none;

                        ConsoleGame.Update(b, Color.black);

                        if (!b.IsBlackCheck)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {

                }//+1;-1


                board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);
                if (board.Squares[y, x].FiguresCanMove.Count > 1)
                    board.IsMoveUnderBlackCheck = 1;
            }
        }

        private static void PawnMove(Square sq, Board board)
        {
            int x = sq.XCoordinate;
            int y = sq.YCoordinate;
            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y, x]);
            if (board.Squares[y, x].Figure == Figure.whitePawn)
            {
                if (!board.IsWhiteCheck)
                {
                    try
                    {
                        if (blackFigures.Contains(board.Squares[y - 1, x - 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);
                            if (board.Squares[y - 1, x - 1].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x - 1] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//Left

                    try
                    {
                        if (blackFigures.Contains(board.Squares[y - 1, x + 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);
                            if (board.Squares[y - 1, x + 1].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x + 1] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//Rigth

                    try
                    {
                        if (board.Squares[y - 1, x].Figure == Figure.none)
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);
                            if (board.Squares[y - 1, x].Figure == Figure.blackKing)
                            {
                                board.IsMoveUnderBlackCheck = 2;
                                board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 1, x] });
                                board.IsBlackCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        //Реалізація зміни фігури
                    }//Forward

                    if (y == 6)
                    {
                        try
                        {
                            if (board.Squares[y - 2, x].Figure == Figure.none)
                            {
                                board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x]);
                                if (board.Squares[y - 2, x].Figure == Figure.blackKing)
                                {
                                    board.IsMoveUnderBlackCheck = 2;
                                    board.BlackCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y - 2, x] });
                                    board.IsBlackCheck = true;
                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            //Реалізація зміни фігури
                        }//Forward 2
                    }
                }
                else
                {
                    if (board.WhiteCheckVector.Count == 1)
                    {
                        #region Coordinates
                        Square[] vect = board.WhiteCheckVector[0];
                        int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                        #endregion


                        try
                        {
                            if (blackFigures.Contains(board.Squares[y - 1, x - 1].Figure))
                            {
                                int result = (x - 1) * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 1), 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - (x - 1), 2) + Math.Pow(yK - (y - 1), 2)));
                                if (result == 0 && dres == 0)
                                {
                                    board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x - 1]);
                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {

                        }//Left

                        try
                        {
                            if (blackFigures.Contains(board.Squares[y - 1, x + 1].Figure))
                            {
                                int result = (x + 1) * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 1), 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - (x + 1), 2) + Math.Pow(yK - (y - 1), 2)));
                                if (result == 0 && dres == 0)
                                {
                                    board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x + 1]);
                                }

                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {

                        }//Rigth

                        try
                        {
                            if (board.Squares[y - 1, x].Figure == Figure.none)
                            {
                                int result = x * (yK - yAt) - (y - 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - x, 2) + Math.Pow(yAt - (y - 1), 2))) - Math.Sqrt((Math.Pow(xK - x, 2) + Math.Pow(yK - (y - 1), 2)));
                                if (result == 0 && dres == 0)
                                {
                                    board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 1, x]);
                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            //Реалізація зміни фігури
                        }//Forward

                        if (y == 6)
                        {
                            try
                            {
                                if (board.Squares[y - 2, x].Figure == Figure.none)
                                {
                                    int result = (x) * (yK - yAt) - (y - 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x), 2) + Math.Pow(yAt - (y - 2), 2))) - Math.Sqrt((Math.Pow(xK - (x), 2) + Math.Pow(yK - (y - 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y - 2, x]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                //Реалізація зміни фігури
                            }//Forward 2
                        }

                    }
                }
            }
            if (board.Squares[y, x].Figure == Figure.blackPawn)
            {
                if (!board.IsBlackCheck)
                {
                    try
                    {
                        if (whiteFigures.Contains(board.Squares[y + 1, x + 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);
                            if (board.Squares[y + 1, x + 1].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x + 1] });
                                board.IsWhiteCheck = true;
                            }

                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//Left

                    try
                    {
                        if (whiteFigures.Contains(board.Squares[y + 1, x - 1].Figure))
                        {
                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);
                            if (board.Squares[y + 1, x - 1].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x - 1] });
                                board.IsWhiteCheck = true;
                            }
                        }

                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }//Rigth

                    try
                    {
                        if (board.Squares[y + 1, x].Figure == Figure.none)
                        {

                            board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);
                            if (board.Squares[y + 1, x].Figure == Figure.whiteKing)
                            {
                                board.IsMoveUnderWhiteCheck = 2;
                                board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 1, x] });
                                board.IsWhiteCheck = true;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        //Реалізація зміни фігури
                    }//Forward

                    if (y == 1)
                    {
                        try
                        {
                            if (board.Squares[y + 2, x].Figure == Figure.none)
                            {
                                board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x]);
                                if (board.Squares[y + 2, x].Figure == Figure.whiteKing)
                                {
                                    board.IsMoveUnderWhiteCheck = 2;
                                    board.WhiteCheckVector.Add(new Square[2] { board.Squares[y, x], board.Squares[y + 2, x] });
                                    board.IsWhiteCheck = true;
                                }
                            }
                        }


                        catch (IndexOutOfRangeException e)
                        {

                        }//Forward 2
                    }
                    if (board.Squares[y, x].FiguresCanMove.Count > 1)
                        board.IsMoveUnderBlackCheck = 1;
                }
                else
                {
                    if (board.BlackCheckVector.Count == 1)
                    {
                        #region Coordinates
                        Square[] vect = board.BlackCheckVector[0];
                        int xAt = vect[0].XCoordinate, yAt = vect[0].YCoordinate, xK = vect[1].XCoordinate, yK = vect[1].YCoordinate;
                        #endregion

                        try
                        {
                            if (whiteFigures.Contains(board.Squares[y + 1, x + 1].Figure))
                            {
                                int result = (x + 1) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x + 1), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x + 1), 2) + Math.Pow(yK - (y + 1), 2)));
                                if (result == 0 && dres == 0)
                                {
                                    board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x + 1]);
                                }

                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {

                        }//Left

                        try
                        {
                            if (whiteFigures.Contains(board.Squares[y + 1, x - 1].Figure))
                            {
                                int result = (x - 1) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x - 1), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x - 1), 2) + Math.Pow(yK - (y + 1), 2)));
                                if (result == 0 && dres == 0)
                                {
                                    board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x - 1]);
                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {

                        }//Rigth

                        try
                        {
                            if (board.Squares[y + 1, x].Figure == Figure.none)
                            {
                                int result = (x) * (yK - yAt) - (y + 1) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x), 2) + Math.Pow(yAt - (y + 1), 2))) - Math.Sqrt((Math.Pow(xK - (x), 2) + Math.Pow(yK - (y + 1), 2)));
                                if (result == 0 && dres == 0)
                                {
                                    board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 1, x]);
                                }
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            //Реалізація зміни фігури
                        }//Forward

                        if (y == 1)
                        {
                            try
                            {
                                if (board.Squares[y + 2, x].Figure == Figure.none)
                                {
                                    int result = (x) * (yK - yAt) - (y + 2) * (xK - xAt) + yAt * (xK - xAt) - xAt * (yK - yAt);
                                    double dres = Math.Sqrt((Math.Pow(xAt - xK, 2) + Math.Pow(yAt - yK, 2))) - Math.Sqrt((Math.Pow(xAt - (x), 2) + Math.Pow(yAt - (y + 2), 2))) - Math.Sqrt((Math.Pow(xK - (x), 2) + Math.Pow(yK - (y + 2), 2)));
                                    if (result == 0 && dres == 0)
                                    {
                                        board.Squares[y, x].FiguresCanMove.Add(board.Squares[y + 2, x]);
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {

                            }//Forward 2
                        }

                        if (board.Squares[y, x].FiguresCanMove.Count > 1)
                            board.IsMoveUnderWhiteCheck = 1;
                    }
                }
            }
        }
    }
}