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
        private int turnNum = 0;
        private Play[] humanPlay = new Play[2];

        public GameManager(Func<Game, int> heuristic1, int ply1, Func<Game, int> heuristic2, int ply2, uint size = 8)
        {
            //8x8 board
            game = new Game(size);
            Agents[0] = new ReversiSolver(TileColor.BLACK, heuristic1, ply1);
            Agents[1] = new ReversiSolver(TileColor.WHITE, heuristic2, ply2);
        }

        public GameManager(Func<Game, int> heuristic, int ply, TileColor color, uint size = 8)
        {
            game = new Game(size);
            Agents[0] = new ReversiSolver(color, heuristic, ply);
        }

        public GameManager(uint size = 8)
        {
            game = new Game(size);
        }

        public void PlayAs(Play p, TileColor color)
        {

            int index = color == TileColor.BLACK ? 0 : 1;
            ReversiSolver agent = Agents[index];

            if(agent != null)
            {
                throw new ArgumentException();
            }

            //if the player is the right color 
            //unsafe, since the human player could play on the AI's behalf
            if (game.IsPlayer1 && color != TileColor.BLACK
                || !game.IsPlayer1 && color != TileColor.WHITE)
            {
                throw new ArgumentException("Not your turn!");
            }

            humanPlay[index] = p;
        }

        public Game GetGame()
        {
            return new Game(game);
        }

        public Game Next()
        {
            int index = game.IsPlayer1 ? 0 : 1;

            ReversiSolver agent = Agents[index];

            //human player
            if (agent == null)
            {
                if (humanPlay[index] == null) return null;
                game.UsePlay(humanPlay[index]);
                humanPlay[index] = null;
            }
            else
            {
                Play p = agent.ChoosePlay(game);
                if (p != null)
                {
                    game.UsePlay(p);
                }
                else
                {
                    //This should never happen.  Game class should handle place where no move possible.
                    throw new ArgumentException();
                }
            }
            return GetGame();
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
