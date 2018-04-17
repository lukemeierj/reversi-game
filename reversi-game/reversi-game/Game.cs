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
            player1 = true;
        }

        public Tile Place(int x, int y)
        {
            Tile placement = board.Place(x, y, player1 ? TileColor.BLACK : TileColor.WHITE);
            if (placement != null){
                player1 = !player1;
            }
            return placement;
        }

        public List<Tuple<int,int>> PlayablePositions()
        {
            List<Tuple<int, int>> possible = board.OpenAdjacentSpots();
            List<Tuple<int, int>> results = new List<Tuple<int, int>>();

            foreach (Tuple<int,int> coord in possible)
            {
                if(IsPlayable(coord.Item1, coord.Item2))
                {
                    results.Add(coord);
                }
            }
            return results;
        }

        public bool IsPlayable(int x, int y)
        {
            TileColor playerColor = player1 ? TileColor.BLACK : TileColor.WHITE;
            TileColor opponentColor = player1 ? TileColor.WHITE : TileColor.BLACK;
            #region horizontals
            for (int ix = x; ix >= 0; ix--)
            {
                //broken, i'm going to bed though
                if(ix == x - 1 && board[ix, y].color == playerColor)
                {
                    break;
                }

                if(board[ix, y].color == playerColor)
                {
                    return true;
                }
            }

            for (int ix = x; ix < board.size; ix++)
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

            for (int iy = y; iy < board.size; iy++)
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

            for (int ix = x; ix < board.size; ix++)
            {
                for (int iy = y; iy < board.size; iy++)
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

            for (int ix = x; ix >= 0; ix--)
            {
                for (int iy = y; iy < board.size; iy++)
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

            for (int ix = x; ix < board.size; ix++)
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

        public bool GameOver()
        {
            return board.BoardFull();
        }

        //player 1 plays black
        public bool player1 { private set; get; }
        private Board board;
    }
}
