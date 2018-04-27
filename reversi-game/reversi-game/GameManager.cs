using System;
using System.Collections.Generic;
using System.Text;
using ReversiAI;

namespace ReversiGame
{
    class GameManager
    {
        private Game game;
        public ReversiSolver[] Agents = new ReversiSolver[2];

        public GameManager(Func<Game, int> heuristic1, int ply1, Func<Game, int> heuristic2, int ply2)
        {
            Agents[0] = new ReversiSolver(TileColor.BLACK, heuristic1, ply1);
            Agents[1] = new ReversiSolver(TileColor.WHITE, heuristic2, ply2);
        }

        public GameManager(Func<Game, int> heuristic, int ply, TileColor color)
        {
            Agents[0] = new ReversiSolver(color, heuristic, ply);
        }

        public IEnumerator<Game> Play()
        {
            int i = 0;
            //not good end state, but avoid infinite loops for now
            while (i <= 70)
            {
                // Play the AI move
                Play q = Agents[i % 2].ChoosePlay(game);

                if (q != null)
                {
                    Console.WriteLine((q.Coords.Item1 + 1) + " " + (q.Coords.Item2 + 1));
                    game.UsePlay(q);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
                yield return game;
            }

        }


    }
}
