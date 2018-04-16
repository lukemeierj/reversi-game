using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi_game
{
    class Board
    {
        public Board(uint size)
        {
            this.board = new Tile[size, size];
            this.size = size;
        }

        public Tile Place(int x, int y, TileColor color)
        {
            if(board[x,y] != null)
            {
                return null;
            }
            Tile placement = new Tile(color);
            placement.Place(x, y);
            board[x, y] = placement;
            return placement;
        }

        public bool BoardFull()
        {
            for(int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if(board[x,y] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<Tuple<int, int>> OpenAdjacentSpots()
        {
            List<Tuple<int, int>> openAdjacent = new List<Tuple<int, int>>();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (AdjacentToTile(x,y))
                    {
                        openAdjacent.Add(Tuple.Create(x, y));
                    }
                }
            }
            return openAdjacent;
        }

        private bool AdjacentToTile(int x, int y)
        {
            for(int ix = Math.Max(0, x-1); ix < x+1 && ix < size; ix++)
            {
                for (int iy = Math.Max(0, y - 1); iy < y + 1 && iy < size; iy++)
                {
                    if (board[ix, iy] != null) return true;
                }

            }
            return false;
        }

        public Tile[,] board { get; private set; }
        private uint size;
    }
}
