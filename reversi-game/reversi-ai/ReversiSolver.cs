using System;
using System.Collections.Generic;
using System.Text;
using ReversiGame;
namespace ReversiAI
{
    /// <summary>
    /// Solves Reversi games using minimax alpha-beta pruning and a given heuristic.
    /// </summary>
    class ReversiSolver
    {
        /// <summary>
        /// Takes a game object and returns the best move given the heuristic for the current player
        /// </summary>
        /// <param name="game">The game in the state that needs to be searched from</param>
        /// <param name="heuristic">Assigns an integer score to a game object for the black player.
        /// Higher numbers are better</param>
        /// <returns></returns>
        public static Play ChoosePlay(Game game, Func<Game, int> heuristic)
        {
            return AlphaBeta(game, heuristic).Item2;
        }

        /// <summary>
        /// Searches the game tree and returns the a pair of the best play for the current player and it's heuristic value
        /// </summary>
        /// <param name="game">The game is in state to be searched from</param>
        /// <param name="heuristic">A function that rates each board on an integer scale in terms of black. Higher is better</param>
        /// <param name="alpha">Highest value seen so far</param>
        /// <param name="beta">Lowest value seen so far</param>
        /// <param name="ply">The depth to search the game</param>
        /// <returns></returns>
        private static Tuple<int,Play> AlphaBeta(Game game, Func<Game,int> heuristic, bool max = true, int alpha = int.MinValue, int beta = int.MaxValue, int ply=5)
        {
            // If exit case
            if(ply == 0 || game.GameOver())
            {
                return new Tuple<int,Play>(heuristic(game),null);
            }
            // if player is black, then try to maximize
            // else, try to minimize
            Func<int, int, int> Optimizer = max ? (Func < int, int, int >)Math.Max : (Func < int, int, int > )Math.Min;

            // if player 1 (max/black), start at the lowest score and try to improve
            // if player 2 (min/white), start at best score and try to decrease score
            int bestScore = max ? int.MinValue : int.MaxValue;

            // The highest value found by the function so far.
            //  Set to the lowest possible value so any value is higher.
            Play bestPlay = null;

            // For each possible game from plays
            // Use Game.ForkGame(Play) to generate different branches
            foreach (KeyValuePair<Tuple<int,int>,Play> pair in game.PossiblePlays())
            {
                // Takes the max between the branch and the current minimum value
                int childScore = AlphaBeta(game.ForkGame(pair.Value), heuristic, !max, alpha, beta, ply - 1).Item1;

                // If the new value is better, save it and the move that yields it
                if (bestScore != Optimizer(bestScore, childScore))
                {
                    bestPlay = pair.Value;
                    bestScore = childScore;
                }

                if (max)
                    alpha = Optimizer(alpha, bestScore);
                else
                    beta = Optimizer(beta, bestScore);

                if (beta <= alpha)
                    break;
            }
            return new Tuple<int, Play>(bestScore, bestPlay);
           
        }


        public static Func<Game, int> GetForColor(Func<Game, TileColor, int> function, TileColor color)
        {
            return (game) => function(game, color);
        }

        /// <summary>
        /// Calculates the heuristic value based on the difference of black tiles and white tiles
        /// </summary>
        /// <param name="game">Game in the state the move needs to be calculated in</param>
        /// <returns></returns>
        public static int BasicHeuristic(Game game, TileColor color)
        {
            int black = 0;
            int white = 0;
            for(int i = 0; i < game.Board.Size; i++)
            {
                for (int j = 0; j < game.Board.Size; j++)
                {
                    switch (game.ColorAt(i, j))
                    {
                        case TileColor.BLACK:
                            black++;
                            break;
                        case TileColor.WHITE:
                            white++;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (game.GameOver())
            {
                if(color == TileColor.BLACK && black > white
                    || color == TileColor.WHITE && white > black)
                {
                    return 100;
                }
                else
                {
                    return 0;
                }
            }

            if (color == TileColor.BLACK)
            {
                return black - white;
            }
            else if (color == TileColor.WHITE)
            {
                return white - black;
            }
            else
                return 0;
        }
    }
}
