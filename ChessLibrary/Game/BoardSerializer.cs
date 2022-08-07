using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;
using System.IO;

namespace ChessLibrary.Game
{
    public class BoardSerializer
    {
        public class BoardSer
        {
            public bool IsWhiteCheck { get; set; }
            public bool IsBlackCheck { get; set; }
            public Square[,] Squares { get; set; }
        }
        

        public BoardSerializer(Board board)
        {
            Boardser = new BoardSer();
            Boardser.IsBlackCheck = board.IsBlackCheck;
            Boardser.IsWhiteCheck = board.IsWhiteCheck;
            Boardser.Squares = board.Squares;
        }
        private BoardSer Boardser { get; set; }


        


        public string SerializeBoardInJson()
        {
            StringBuilder jsonStringBuilder = new StringBuilder();

            jsonStringBuilder.Append("{ \n");
            jsonStringBuilder.Append($"\t\"IsWhiteCheck\" : \"{Boardser.IsWhiteCheck}\",\n");
            jsonStringBuilder.Append($"\t\"IsBlackCheck\" : \"{Boardser.IsBlackCheck}\",\n");
            jsonStringBuilder.Append("\t\"Squares\" : [\n");
            for (int i = 0; i<8; i++)
            {
                for(int j = 0; j<8; j++)
                {
                    jsonStringBuilder.Append("\t\t{ \n");

                    jsonStringBuilder.Append($"\t\t\"Figure\" : \"{Boardser.Squares[i,j].Figure}\",\n");
                    jsonStringBuilder.Append($"\t\t\"XCoordinate\" : \"{Boardser.Squares[i,j].XCoordinate}\",\n");
                    jsonStringBuilder.Append($"\t\t\"YCoordinate\" : \"{Boardser.Squares[i, j].YCoordinate}\",\n");
                    jsonStringBuilder.Append("\t\t\"FiguresCanMove\" : [\n");
                    for( int k = 0; k<Boardser.Squares[i,j].FiguresCanMove.Count;k++)
                    {
                        Square sq = Boardser.Squares[i, j].FiguresCanMove[k];                        
                            jsonStringBuilder.Append("\t\t\t{\n");
                            jsonStringBuilder.Append($"\t\t\t\"XCoordinate\" : \"{sq.XCoordinate}\",\n");
                            jsonStringBuilder.Append($"\t\t\t\"YCoordinate\" : \"{sq.YCoordinate}\"\n");

                        if(k != Boardser.Squares[i, j].FiguresCanMove.Count-1)
                            jsonStringBuilder.Append("\t\t\t},\n");          
                        else
                            jsonStringBuilder.Append("\t\t\t}\n");
                    }
                        jsonStringBuilder.Append("\t\t]\n");
                    if (i+j!= 14)
                        jsonStringBuilder.Append("},\n");
                    else
                        jsonStringBuilder.Append("}\n");
                }
            }
            jsonStringBuilder.Append("\t]\n");
            jsonStringBuilder.Append("}\n");
            
            using (StreamWriter writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\board.json"))
            {
                writer.WriteLine(jsonStringBuilder.ToString());
            }           
                  
            
            return jsonStringBuilder.ToString();
        }
    }
}
