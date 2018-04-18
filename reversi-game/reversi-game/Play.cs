using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame
{
    // Stores the affected tiles
    class Play
    {
        public Play(TileColor color, Tuple<int, int> coords, List<Tile> affected)
        {
            this.Color = color;
            this.Coords = coords;
            this.AffectedTiles = affected;            
        }

        public Play(TileColor color, Tuple<int, int> coords)
        {
            this.Color = color;
            this.Coords = coords;
        }

        public Play(Board board, TileColor color, Tuple<int, int> coords)
        {
            //store given information
            this.Color = color;
            this.Coords = coords;

            // -------------------------------------
            // calculate affected tiles 


            //takes play
            TileColor playerColor = color;
            TileColor opponentColor = color == TileColor.BLACK ? TileColor.WHITE : TileColor.BLACK;

            int x = coords.Item1;
            int y = coords.Item2;

            //generate "ray" to check in each direction
            //  if the first tile on the ray isn't an opponent's, stop persuing that ray
            for (double theta = 0.0f; theta < 2 * Math.PI; theta += (Math.PI / 4))
            {
                //a list of all the tiles in this ray
                // add to the affectTiles of the play is valid in this direction
                List<Tile> rayTiles = new List<Tile>();

                // Defines the ray to look along
                int dx = (int)Math.Round(Math.Cos(theta), MidpointRounding.AwayFromZero);
                int dy = (int)Math.Round(Math.Sin(theta), MidpointRounding.AwayFromZero);

                // Keeps track of the current position in the ray
                int ix = x + dx;
                int iy = y + dy;

                // while the ray is in bounds
                while (ix < board.Size && ix >= 0 && iy < board.Size && iy >= 0)
                {

                    // Break if an empty tile is found
                    if (board[ix, iy] == null)
                    {
                        break;
                    }

                    //only check the ray if it has an opponent tile as the start of the ray
                    if (ix == x + dx && iy == y + dy && board[ix, iy].color != opponentColor)
                    {
                        break;
                    }


                    // If a player tile is found after an opponent tile, (x,y) is a valid move
                    if (board[ix, iy].color == playerColor)
                    {
                        this.AddAffected(rayTiles);
                        break;
                    }

                    rayTiles.Add(board[ix, iy]);

                    ix += dx;
                    iy += dy;
                }
            }
        }

        public void AddAffected(List<Tile> affected)
        {
            if (AffectedTiles == null)
            {
                AffectedTiles = new List<Tile>();
            }
            AffectedTiles.AddRange(affected);
        }

        public void AddAffected(Tile affectedTile)
        {
            if (AffectedTiles == null)
            {
                AffectedTiles = new List<Tile>();
            }
            AffectedTiles.Add(affectedTile);
        }

        public TileColor Color { private set; get; }
        public Tuple<int, int> Coords { private set; get; }
        public List<Tile> AffectedTiles { private set; get; }
    }
}
