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
            board = new Tile[size, size];
        }

        public void Place(int x, int y, TileColor color)
        {
            Tile placement = new Tile(color);
            placement.Place(x, y);
            board[x, y] = placement;
        }

        private Tile[,] board;
    }
}
