using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi_game
{

    //TODO:
    /*  Not necessarily in this class...
     * If one player can't make a move, alert game and let the other player go instead
     * Check for possible moves, be able to get() possible moves
     * Make grid pretty
     * Show in UI which player's turn it is
     * When a play is made, flip the relevant tiles
     * 
     */
    class Game
    {
        public Game(uint size)
        {
            board = new Board(size);
            isPlayer1 = true;
        }

        //place a tile at a position
        // the color of the tile is determined by whose turn it is
        public Tile Place(int x, int y)
        {
            Tile placement = board.Place(x, y, isPlayer1 ? TileColor.BLACK : TileColor.WHITE);
            if (placement != null){
                isPlayer1 = !isPlayer1;
            }
            return placement;
        }


        //find all possible plays given the current game state 
        // this takes into consideration whose turn it is
        public List<Tuple<int,int>> PossiblePlays()
        {
            //for all open spots on the board that are adjacent to any tiles
            List<Tuple<int, int>> possible = board.OpenAdjacentSpots();
            List<Tuple<int, int>> results = new List<Tuple<int, int>>();


            foreach (Tuple<int,int> coord in possible)
            {
                //if a given coordinate is a playable move, add it's coordinates
                if(IsPlayable(coord.Item1, coord.Item2))
                {
                    results.Add(coord);
                }
            }
            return results;
        }

        //determine if a position is playable given which player's turn it is
        public bool IsPlayable(int x, int y)
        {
            //takes play
            TileColor playerColor = isPlayer1 ? TileColor.BLACK : TileColor.WHITE;
            TileColor opponentColor = isPlayer1 ? TileColor.WHITE : TileColor.BLACK;

            //generate "ray" to check in each direction
            //  if the first tile on the ray isn't an opponent's, stop persuing that ray
            for(double theta = 0.0f; theta < 2*Math.PI; theta += (Math.PI / 4))
            {
                // Defines the ray to look along
                int dx = (int) Math.Round(Math.Cos(theta), MidpointRounding.AwayFromZero);
                int dy = (int) Math.Round(Math.Sin(theta), MidpointRounding.AwayFromZero);

                // Keeps track of the current position in the ray
                int ix = x + dx;
                int iy = y + dy;

                // while the ray is in bounds
                while (ix < board.Size && ix >= 0 && iy < board.Size && iy >= 0)
                {

                    //only check the ray if it has an opponent tile as the start of the ray
                    if (board[x + dx, y + dy] != null && board[x + dx, y + dy].color != opponentColor)
                    {
                        break;
                    }

                    // Break if an empty tile is found
                    if(board[ix, iy] == null)
                    {
                        break;
                    }

                    // If a player tile is found after an opponent tile, (x,y) is a valid move
                    if (board[ix, iy].color == playerColor)
                    {
                        return true;
                    }

                    ix += dx;
                    iy += dy;
                }                
            }

            return false;
        }

        //right now, our only game over condition is a full board.
        public bool GameOver()
        {
            return board.BoardFull();
        }

        //player 1 plays black.
        //when true, the active player is player1.
        public bool isPlayer1 { private set; get; }
        private Board board;
    }
}
