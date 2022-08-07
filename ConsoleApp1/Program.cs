using System;
using ChessLibrary.Game;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            game.Update();
            BoardSerializer serializer = new BoardSerializer(game.Board);
           serializer.SerializeBoardInJson();
        }
    }
}

