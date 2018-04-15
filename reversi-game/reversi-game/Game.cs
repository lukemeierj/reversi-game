using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi_game
{
    class Game
    {
        public Game(uint size)
        {
            board = new Board(size);
            player1 = true;
        }

        public void Place(int x, int y)
        {
            board.Place(x, y, player1 ? TileColor.BLACK : TileColor.WHITE);
            player1 = !player1;
        }

        //player 1 plays black
        public bool player1 { private set; get; }
        private Board board;
    }
}
