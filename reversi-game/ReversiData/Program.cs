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

            var results = TestHeuristic(ReversiSolver.BasicHeuristic, ReversiSolver.ActualMobilityHeuristic, 10, 5, 5, 6);
            Console.WriteLine("Black: " + results.Item1 + " | White: " + results.Item2);
            Console.ReadLine();
        }

        static Tuple<int,int> TestHeuristic(Func<Game, TileColor, int> h1, Func<Game, TileColor, int> h2, int trials, int ply1 = 5, int ply2 = 5, uint size = 8)
        {
            int blackWins = 0;
            int whiteWins = 0;
            GameManager manager = new GameManager(h1, ply1, h2, ply2, size);
            for(int i = 0; i < trials; i++)
            {
                manager.Reset();
                Game game = manager.GetGame();
                while(game.Winner == null)
                {
                    game = manager.Next();
                }
                if (game.Winner == TileColor.BLACK)
                {
                    blackWins++;
                    Console.WriteLine(i + "\tBlack wins");
                }
                else if (game.Winner == TileColor.WHITE)
                {
                    whiteWins++;
                    Console.WriteLine(i + "\tWhite wins");
                }
            }
            return Tuple.Create(blackWins, whiteWins);
        }
    }
}
