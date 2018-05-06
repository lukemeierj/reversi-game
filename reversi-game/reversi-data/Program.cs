using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReversiGame;
using ReversiAI;
using System.IO;

namespace ReversiData
{
    class Program
    {
        static void Main(string[] args)
        {
            uint size = 8;
            string filename = "results.txt";
            

            Tuple<int, int> results;

            List<Tuple<Func<Game, TileColor, int>, string>> heuristics = new List<Tuple<Func<Game, TileColor, int>, string>>
            {
                Tuple.Create<Func<Game, TileColor, int>,string>(ReversiSolver.TileCountHeuristic,"Count"),
                Tuple.Create<Func<Game, TileColor, int>,string>(ReversiSolver.ActualMobilityHeuristic,"Mobility"),
                Tuple.Create<Func<Game, TileColor, int>,string>(ReversiSolver.CornersHeuristic,"Corners"),
                Tuple.Create<Func<Game, TileColor, int>,string>(ReversiSolver.WeightedHeuristic,"Weighted")

            };

            for(int ply = 2; ply < 9; ply++)
            {
                StreamWriter output = new StreamWriter("ply" + ply + ".txt");
                Console.WriteLine("Ply:" + ply);
                output.WriteLine("Ply: " + ply);
                foreach (var blackHeuristic in heuristics)
                {
                    foreach (var whiteHeuristic in heuristics)
                    {
                        Console.WriteLine("Testing: " + blackHeuristic.Item2 + " against " + whiteHeuristic.Item2);
                        results = TestHeuristic(blackHeuristic.Item1, whiteHeuristic.Item1, ply, ply, size);
                        Tuple<Func<Game, TileColor, int>, string> winner;

                        if (results.Item1 > results.Item2)
                        {
                            winner = blackHeuristic;
                        }
                        else
                        {
                            winner = whiteHeuristic;
                        }

                        output.Write(winner.Item2 + " " + results.Item1 + "-" + results.Item2 + '\t');
                    }
                    output.Write('\n');
                }
                output.Close();
            }
            

            
        }

        static Tuple<int,int> TestHeuristic(Func<Game, TileColor, int> h1, Func<Game, TileColor, int> h2, int ply1 = 5, int ply2 = 5, uint size = 8)
        {
            int black = 0;
            int white = 0;
            int counter = 0;
            GameManager manager = new GameManager(h1, ply1, h2, ply2, size);
            manager.Reset();
            Game game = manager.GetGame();
            while(!game.GameOver())
            {
                if(counter > 100)
                {
                    System.Console.WriteLine("AHHHHHH");
                    break;
                }
                game = manager.Next();
                counter++;
            }

            black = game.Board.GetNumColor(TileColor.BLACK);
            white = game.Board.GetNumColor(TileColor.WHITE);
            return Tuple.Create(black, white);
        }
    }
}
