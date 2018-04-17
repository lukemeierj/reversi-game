using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi_game
{
    // Stores the affected tiles
    class Play
    {
        public Play(Board board, TileColor color, Tuple<int,int> coords)
        {
            this.color = color;
            this.coords = coords;

            
        }
        TileColor color;
        Tuple<int, int> coords;
        List<Tile> affectedTiles;
    }
}
