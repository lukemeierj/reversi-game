using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi_game
{
    enum TileColor
    {
        WHITE=0,
        BLACK=1,
    }

    class Tile
    {
        //If the tile is white, flip it to black and vis versa 
        public TileColor Flip()
        {
            if (color == TileColor.WHITE)
            {
                color = TileColor.BLACK;
            }
            else
            {
                color = TileColor.WHITE;
            }
            return color;
        }

        //place a tile in an x,y coordinate.  
        public Tuple<int, int> Place(int x, int y)
        {
            //tiles cannot be placed twice.
            if (placed) throw new System.InvalidOperationException("Tile already placed!");
            //store the final position for the tile 
            coords = Tuple.Create<int, int>(x, y);
            placed = true;
            return coords;
        }

        public Tile(TileColor color)
        {
            this.color = color;
            coords = Tuple.Create(-1, -1);
        }


        public TileColor color { get; private set; }
        private bool placed = false;
        private Tuple<int, int> coords;
    }
}
