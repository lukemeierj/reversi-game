using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame
{
    class Game
    {
        //player 1 plays black.
        //when true, the active player is player1.
        public bool IsPlayer1 { private set; get; }
        public Board Board { private set; get; }
        private bool deadlock = false;
        public TileColor? Winner
        {
            get {
                if (GameOver())
                {
                    if(Board.GetNumColor(TileColor.BLACK) > Board.GetNumColor(TileColor.WHITE))
                    {
                        return TileColor.BLACK;
                    } else if (Board.GetNumColor(TileColor.WHITE) > Board.GetNumColor(TileColor.BLACK))
                    {
                        return TileColor.WHITE;
                    } else
                    {
                        return null;
                    }
                } else if (deadlock)
                {
                    return TileColor.BLANK;
                }
                else
                {
                    return null;
                }
            }
        }

        public Game(uint size)
        {

            Board = new Board(size);
            IsPlayer1 = true;
            int x = ((int)size - 1) / 2;
            int y = ((int)size - 1) / 2;

            //place first four tiles
            Place(x, y++);
            Place(x++, y);
            Place(x, y--);
            Place(x, y);
        }

        public Game(Game prevGame)
        {
            Board = new Board(prevGame.Board);
            IsPlayer1 = prevGame.IsPlayer1;
        }

        //place a tile at a position
        // the color of the tile is determined by whose turn it is
        private Tile Place(int x, int y)
        {
            Tile placement = Board.Place(x, y, IsPlayer1 ? TileColor.BLACK : TileColor.WHITE);
            if (placement != null){
                NextTurn();
            }
            return placement;
        }

        private void NextTurn()
        {
            IsPlayer1 = !IsPlayer1;
        }

        //play a play at a given position
        public void UsePlay(Play p)
        {
            Place(p.Coords.Item1, p.Coords.Item2);
            foreach(Tile tile in p.AffectedTiles)
            {
                Board[tile.Coords.Item1, tile.Coords.Item2].Flip();
            }
            int i = 0;
            // Handle case where next player has no moves
            while(i <= 2 && PossiblePlays().Count == 0)
            {
                i++;
                NextTurn();
            }
            if (i >= 2) deadlock = true;
        }

        /// <summary>
        /// Returns a new game advanced by the single move play
        /// </summary>
        /// <param name="play">Move to increment the game by</param>
        /// <returns>A new game advanced by move play</returns>
        public Game ForkGame(Play play)
        {
            Game g = new Game(this);
            g.UsePlay(play);
            return g;
        }

        public TileColor ColorAt(int x, int y)
        {
            if (Board[x, y] == null) return TileColor.BLANK;
            return Board[x, y].color;
        }


        //find all possible plays given the current game state 
        // this takes into consideration whose turn it is
        public Dictionary<Tuple<int, int>, Play> PossiblePlays(bool otherPlayer = false)
        {
            //for all open spots on the board that are adjacent to any tiles
            List<Tuple<int, int>> possiblePositions = Board.OpenAdjacentSpots();
            Dictionary<Tuple<int, int>, Play> results = new Dictionary<Tuple<int, int>, Play>();

            TileColor playerColor = IsPlayer1 ? TileColor.BLACK : TileColor.WHITE;

            // If calcualting potential moves for the other player switch the color
            if (otherPlayer)
            {
                playerColor = IsPlayer1 ? TileColor.WHITE : TileColor.BLACK; ;
            }

            foreach (Tuple<int,int> coord in possiblePositions)
            {

                Play possiblePlay = new Play(Board, playerColor, coord);

                if(possiblePlay.AffectedTiles != null)
                {
                    results.Add(coord, possiblePlay);
                }
            }
            return results;
        }

      
        //right now, our only game over condition is a full board.
        public bool GameOver()
        {
            return deadlock || Board.BoardFull();
        }

        public uint Size()
        {
            return Board.Size;
        }


    }
}
