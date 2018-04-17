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

            //@TODO:  This can probably be consolidated using a function

            #region horizontals
            //check if there are any tiles the move can flip to the left
            for (int ix = x; ix >= 0; ix--)
            {
                //broken, i'm going to bed though
                
                //if the adjacent tile is the same color you're trying to place
                //   this would not be a valid play.  
                if(ix == x - 1 && board[ix, y].color == playerColor)
                {
                    break;
                }

                //if one of the same color book ends the line, then this is a valid play
                if(board[ix, y].color == playerColor)
                {
                    return true;
                }
            }

            //check if there are any tiles the move can flip to the right
            for (int ix = x; ix < board.Size; ix++)
            {
                if (ix == x + 1 && board[ix, y].color == playerColor)
                {
                    break;
                }

                if (board[ix, y].color == playerColor)
                {
                    return true;
                }
            }
            #endregion
            #region verticals
            //check if there are any tiles the move can flip above
            for (int iy = y; iy >= 0; iy--)
            {
                if (iy == y - 1 && board[x, iy].color == playerColor)
                {
                    break;
                }

                if (board[x, iy].color == playerColor)
                {
                    return true;
                }
            }

            //check if there are any tiles the move can flip below
            for (int iy = y; iy < board.Size; iy++)
            {
                if (iy == y + 1 && board[x, iy].color == playerColor)
                {
                    break;
                }

                if (board[x, iy].color == playerColor)
                {
                    return true;
                }
            }
            #endregion

            //check if there are any tiles the move can flip moving diagonally towards corner at 0,0
            for (int ix = x; ix >= 0; ix--)
            {
                for (int iy = y; iy >= 0; iy--)
                {
                    if (iy == y - 1 && ix == x -1 && board[ix, iy].color == playerColor)
                    {
                        break;
                    }

                    if (board[ix, iy].color == playerColor)
                    {
                        return true;
                    }
                }
            }
            //check if there are any tiles the move can flip moving diagonally towards corner at size-1,size-1
            for (int ix = x; ix < board.Size; ix++)
            {
                for (int iy = y; iy < board.Size; iy++)
                {
                    if (iy == y + 1 && ix == x + 1 && board[ix, iy].color == playerColor)
                    {
                        break;
                    }

                    if (board[ix, iy].color == playerColor)
                    {
                        return true;
                    }
                }
            }
            //check if there are any tiles the move can flip moving diagonally towards corner at 0,size-1
            for (int ix = x; ix >= 0; ix--)
            {
                for (int iy = y; iy < board.Size; iy++)
                {
                    if (iy == y + 1 && ix == x - 1 && board[ix, iy].color == playerColor)
                    {
                        break;
                    }

                    if (board[ix, iy].color == playerColor)
                    {
                        return true;
                    }
                }
            }
            //check if there are any tiles the move can flip moving diagonally towards corner at size-1,0
            for (int ix = x; ix < board.Size; ix++)
            {
                for (int iy = y; iy >= 0; iy--)
                {
                    if (iy == y - 1 && ix == x + 1 && board[ix, iy].color == playerColor)
                    {
                        break;
                    }

                    if (board[ix, iy].color == playerColor)
                    {
                        return true;
                    }
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
