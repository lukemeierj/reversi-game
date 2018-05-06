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

        #region heuristics

        /// <summary>
        /// Calculates the heuristic value based on the difference of black tiles and white tiles
        /// </summary>
        /// <param name="game">Game in the state the move needs to be calculated in</param>
        /// <returns></returns>
        public static int TileCountHeuristic(Game game, TileColor color)
        {
            int black = game.Board.GetNumColor(TileColor.BLACK);
            int white = game.Board.GetNumColor(TileColor.WHITE);

            if(black + white == 0)
            {
                return 0;
            }

            if (color == TileColor.BLACK)
            {
                return 100 * (black - white) / (black + white);
            }
            else if (color == TileColor.WHITE)
            {
                return 100 * (white - black) / (black + white);
            }

            return 0;
        }
        
        /// <summary>
        /// Calculates a heuristic based on how many moves a player has relative to how many moves the opponent has
        /// </summary>
        /// <param name="game"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int ActualMobilityHeuristic(Game game, TileColor color)
        {
            TileColor currentPlayer = game.IsPlayer1 ? TileColor.BLACK : TileColor.WHITE;
            TileColor maxPlayer = color;
            TileColor minPlayer = color == TileColor.BLACK ? TileColor.WHITE : TileColor.BLACK;

            int maxMobility = 0;
            int minMobility = 0;

            // Sets the max and min mobilities
            if(currentPlayer == maxPlayer)
            {
                maxMobility = game.PossiblePlays().Count;
                minMobility = game.PossiblePlays(true).Count;
            } else
            {
                maxMobility = game.PossiblePlays(true).Count;
                minMobility = game.PossiblePlays().Count;
            }
            
            // Return the normalized difference between the mobilities
            if((maxMobility + minMobility) > 0)
            {
                return 100 * (maxMobility - minMobility) / (maxMobility + minMobility);
            } else
            {
                return 0;
            }

        }

        /// <summary>
        /// Calculates a score based on how many more corners one player has than the other
        /// </summary>
        /// <param name="game"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int CornersHeuristic(Game game, TileColor color)
        {
            TileColor currentPlayer = game.IsPlayer1 ? TileColor.BLACK : TileColor.WHITE;

            List<Tuple<int, int>> corners = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 0),
                Tuple.Create((int) game.Board.Size - 1, 0),
                Tuple.Create(0, (int) game.Board.Size - 1),
                Tuple.Create((int) game.Board.Size - 1, (int) game.Board.Size - 1)
            };

            int maxScore = 0, minScore = 0;
            foreach(Tuple<int,int> corner in corners)
            {
                if (game.Board[corner.Item1, corner.Item2] != null)
                {
                    if (game.Board[corner.Item1, corner.Item2].color == currentPlayer)
                    {
                        maxScore++;
                    }
                    else if (game.Board[corner.Item1, corner.Item2].color != TileColor.BLANK)
                    {
                        minScore++;
                    }
                }
            }
            int score = 0;
            if(maxScore + minScore > 0) score = (100 * (maxScore - minScore)) / (minScore + maxScore);

            return score;
        }

        /// <summary>
        /// Calculates the score of a board based on a set of pre-defined weights
        /// </summary>
        /// <param name="game"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int WeightedHeuristic(Game game, TileColor color)
        {
            if (game.Size() != 8)
            {
                throw new ArgumentException("The game board must be size 8x8 for the weighted heuristic");
            }

            int[,] weights = new int[(int) game.Size(),(int) game.Size()];
            int score = 0;

            #region weights
            weights[0, 0] = 4;
            weights[0, 1] = -3;
            weights[0, 2] = 2;
            weights[0, 3] = 2;

            weights[1, 0] = -3;
            weights[1, 1] = -4;
            weights[1, 2] = -1;
            weights[1, 3] = -1;

            weights[2, 0] = 2;
            weights[2, 1] = -1;
            weights[2, 2] = 1;
            weights[2, 3] = 0;

            weights[3, 0] = 2;
            weights[3, 1] = -1;
            weights[3, 2] = 0;
            weights[3, 3] = 1;
            #endregion


            for (int i = 0; i < game.Size(); i++)
            {
                for (int j = 0; j < game.Size(); j++)
                {
                    // Mirrors the weights to all 4 corners
                    int im = i < 4 ? i : 3 - i % 4;
                    int jm = j < 4 ? j : 3 - j % 4;
                    
                    if (game.Board[i, j] != null)
                    {
                        if (game.Board[i, j].color == color)
                        {
                            score += weights[im, jm];
                        }
                        else if(game.Board[i,j].color != TileColor.BLANK)
                        {
                            score -= weights[im, jm];
                        }
                    }
                }
            }


            return score;
        }
    }

    #endregion
}
