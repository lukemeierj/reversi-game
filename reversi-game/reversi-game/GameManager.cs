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
        private Play[] humanPlay = new Play[2];


        //construct manager for computer vs computer play
        public GameManager(Func<Game, TileColor, int> heuristic1, int ply1, Func<Game, TileColor, int> heuristic2, int ply2, uint size = 8)
        {
            game = new Game(size);
            Agents[0] = new ReversiSolver(TileColor.BLACK, heuristic1, ply1);
            Agents[1] = new ReversiSolver(TileColor.WHITE, heuristic2, ply2);
        }

        //construct manager for human vs computer play
        public GameManager(Func<Game, TileColor, int> heuristic, int ply, TileColor color, uint size = 8)
        {
            game = new Game(size);
            int index = color == TileColor.BLACK ? 0 : 1;
            Agents[index] = new ReversiSolver(color, heuristic, ply);
        }

        //create game manager for human vs human play
        public GameManager(uint size = 8)
        {
            game = new Game(size);
        }

        
        //play on behalf of a human
        //play must be valid given the current state of the board 
        public Play OutsidePlay(Play p)
        {
            if (p == null) return null;
            int index = p.Color == TileColor.BLACK ? 0 : 1;
            ReversiSolver agent = Agents[index];

            //if you try to play on behalf of an AI
            if (agent != null)
            {
                Console.WriteLine("Tried to play as AI");
                return null;
            }


            //if the player is the right color 
            //unsafe, since the human player could play on the AI's behalf
            if (game.IsPlayer1 && p.Color != TileColor.BLACK
                || !game.IsPlayer1 && p.Color != TileColor.WHITE)
            {
                throw new ArgumentException("Not your turn!");
            }

            humanPlay[index] = p;
            return p;
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
                //new play should remove all other queued plays
                humanPlay[0] = null;
                humanPlay[1] = null;
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

        public void Reset()
        {
            game = new Game(game.Size());
        }

    }
}
