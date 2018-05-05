using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReversiGame;
using ReversiAI;

namespace ReversiData
{
    class Program
    {
        static void Main(string[] args)
        {
            int ply = 5;
            uint size = 8;

            Tuple<int, int> results;

            //for(int i = 1; i <= 5; i++)
            //{
            //    for(int j = 1; j <= 5; j++)
            //    {
            //        results = TestHeuristic(ReversiSolver.TileCountHeuristic, ReversiSolver.ActualMobilityHeuristic, i, j, size);
            //        Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + i + "vs" + j + " Size: " + size + "\n");
            //    }
            //}

            //Console.WriteLine("Corners vs. Count");
            //results = TestHeuristic(ReversiSolver.CornersHeuristic, ReversiSolver.TileCountHeuristic, 8, 5, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Count vs. Mobility");
            //results = TestHeuristic(ReversiSolver.TileCountHeuristic, ReversiSolver.ActualMobilityHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Count vs. Corners");
            //results = TestHeuristic(ReversiSolver.TileCountHeuristic, ReversiSolver.CornersHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Count vs. Weighted");
            //results = TestHeuristic(ReversiSolver.TileCountHeuristic, ReversiSolver.WeightedHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("\n ----------------------------------- \n");

            //Console.WriteLine("Mobility vs. Tile");
            //results = TestHeuristic(ReversiSolver.ActualMobilityHeuristic, ReversiSolver.TileCountHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Mobility vs. Corners");
            //results = TestHeuristic(ReversiSolver.ActualMobilityHeuristic, ReversiSolver.CornersHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Mobility vs. Weighted");
            //results = TestHeuristic(ReversiSolver.ActualMobilityHeuristic, ReversiSolver.WeightedHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("\n ----------------------------------- \n");


            Console.WriteLine("Corners vs. Count");
            results = TestHeuristic(ReversiSolver.CornersHeuristic, ReversiSolver.TileCountHeuristic, ply, ply, size);
            Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            Console.WriteLine("Corners vs. Mobility");
            results = TestHeuristic(ReversiSolver.CornersHeuristic, ReversiSolver.ActualMobilityHeuristic, ply, ply, size);
            Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            Console.WriteLine("Corners vs. Weighted");
            results = TestHeuristic(ReversiSolver.CornersHeuristic, ReversiSolver.WeightedHeuristic, ply, ply, size);
            Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            Console.WriteLine("\n ----------------------------------- \n");


            //Console.WriteLine("Weighted vs. Count");
            //results = TestHeuristic(ReversiSolver.WeightedHeuristic, ReversiSolver.TileCountHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Weighted vs. Mobility");
            //results = TestHeuristic(ReversiSolver.WeightedHeuristic, ReversiSolver.ActualMobilityHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");

            //Console.WriteLine("Weighted vs. Corners");
            //results = TestHeuristic(ReversiSolver.WeightedHeuristic, ReversiSolver.CornersHeuristic, ply, ply, size);
            //Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size + "\n");


            Console.ReadLine();
        }

        static Tuple<int,int> TestHeuristic(Func<Game, TileColor, int> h1, Func<Game, TileColor, int> h2, int ply1 = 5, int ply2 = 5, uint size = 8)
        {
            int black = 0;
            int white = 0;
            GameManager manager = new GameManager(h1, ply1, h2, ply2, size);
            manager.Reset();
            Game game = manager.GetGame();
            while(!game.GameOver())
            {
                game = manager.Next();
            }

            black = game.Board.GetNumColor(TileColor.BLACK);
            white = game.Board.GetNumColor(TileColor.WHITE);
            return Tuple.Create(black, white);
        }
    }
}
