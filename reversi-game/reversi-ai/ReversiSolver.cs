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
        private Func<Game, int> heuristic;
        public int MaxPly { private set; get; }
        public TileColor Color { private set; get; }

        public ReversiSolver(TileColor color, Func<Game, TileColor, int> heuristic, int ply)
        {
            Color = color;
            SetHeuristic(heuristic);
            MaxPly = ply;
        }


        /// <summary>
        /// Takes a game object and returns the best move given the heuristic for the current player
        /// </summary>
        /// <param name="game">The game in the state that needs to be searched from</param>
        /// <param name="prune">Whether or not to use the alpha beta pruning</param>
        /// <returns>The best play the AI can find at its ply</returns>
        public Play ChoosePlay(Game game, bool prune = true)
        {
            game = new Game(game);
            return AlphaBeta(game, MaxPly, prune).Item2;
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
        private Tuple<int,Play> AlphaBeta(Game game, int ply = 5, bool prune = true, bool max = true, int alpha = int.MinValue, int beta = int.MaxValue)
        {
            //don't evaluate possible plays if you are at the base of the search tree
            Dictionary<Tuple<int,int>, Play> possiblePlays = ply == 0 ? null : game.PossiblePlays();

            // If exit case
            // score not effected by being pushed into a position with no possible moves
            if(ply == 0 || game.GameOver() || possiblePlays.Count == 0)
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
                int childScore = AlphaBeta(game.ForkGame(pair.Value), ply - 1, prune, !max, alpha, beta).Item1;

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

                if (prune && (beta <= alpha))
                    break;
            }
            return new Tuple<int, Play>(bestScore, bestPlay);
           
        }

        /// <summary>
        /// Sets the heuristic function for a solver
        /// </summary>
        /// <param name="heuristic">This is the function, taking game and color</param>
        /// <param name="color">The color this solver will play as</param>
        public void SetHeuristic(Func<Game, TileColor, int> heuristic)
        {
            this.heuristic = (game) => heuristic(game, Color);
        }

        /// <summary>
        /// Calculates the heuristic value based on the difference of black tiles and white tiles
        /// </summary>
        /// <param name="game">Game in the state the move needs to be calculated in</param>
        /// <returns></returns>
        public static int BasicHeuristic(Game game, TileColor color)
        {
            int black = game.Board.GetNumColor(TileColor.BLACK);
            int white = game.Board.GetNumColor(TileColor.WHITE);

            // Ensures winning states are always weighted higher than intermediate states
            if (game.Winner == color)
            {
                return (int)game.Board.Size * (int)game.Board.Size;
            }
            else if (game.Winner != null)
            {
                return -(int)game.Board.Size * (int)game.Board.Size;
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
        
        /// <summary>
        /// Calculates the number of moves the opponent
        /// </summary>
        /// <param name="game"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int ActualMobilityHeuristic(Game game, TileColor color)
        {
            TileColor currentPlayer = game.IsPlayer1 ? TileColor.BLACK : TileColor.WHITE;

            // Ensures winning states are always weighted higher than intermediate states
            if (game.Winner == color)
            {
                return (int)game.Board.Size * (int)game.Board.Size;
            } else if(game.Winner != null)
            {
                return - (int)game.Board.Size * (int)game.Board.Size;
            }

            // Returns the number of plays the opponent can make subtracted from the size * 2
            if(color == currentPlayer)
            {
                return ((int) game.Board.Size * 2) - game.PossiblePlays(true).Count;
            } else
            {
                return ((int)game.Board.Size * 2) - game.PossiblePlays().Count;
            }
            
        }
    }
}
