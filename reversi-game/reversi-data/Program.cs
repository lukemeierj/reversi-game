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
            int ply = 7;
            uint size = 8;

            var results = TestHeuristic(ReversiSolver.CornersHeuristic, ReversiSolver.ActualMobilityHeuristic, ply, ply, size);
            Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2 + " Ply: " + ply + " Size: " + size);
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
