using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi_game
{

    //TODO:
    /*
     * If one player can't make a move, alert game and let the other player go instead
     * Check for possible moves, be able to get() possible moves
     * Dynamically create grid for game
     * Make grid pretty
     * On clicking a grid element, play correct user
     * When a play is made, flip the appropriote tiles
     * Check when game finished 
     * 
     */
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
        //TODO: Not dummy function
        public bool GameOver()
        {
            return false;
        }

        //player 1 plays black
        public bool player1 { private set; get; }
        private Board board;
    }
}
