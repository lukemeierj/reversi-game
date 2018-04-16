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


        public bool GameOver()
        {
            return board.BoardFull();
        }

        //player 1 plays black
        public bool player1 { private set; get; }
        private Board board;
    }
}
