﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame
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
        //player 1 plays black.
        //when true, the active player is player1.
        public bool isPlayer1 { private set; get; }
        private Board board;

        public Game(uint size)
        {
            board = new Board(size);
            isPlayer1 = true;
        }

        public Game(Game prevGame)
        {
            this.board = new Board(prevGame.board);
            this.isPlayer1 = prevGame.isPlayer1;
        }

        //place a tile at a position
        // the color of the tile is determined by whose turn it is
        public Tile Place(int x, int y)
        {
            Tile placement = board.Place(x, y, isPlayer1 ? TileColor.BLACK : TileColor.WHITE);
            if (placement != null){
                isPlayer1 = !isPlayer1;
            }
            return placement;
        }

        //play a play at a given position
        public void UsePlay(Play p)
        {
            Place(p.Coords.Item1, p.Coords.Item2);
            foreach(Tile tile in p.AffectedTiles)
            {
                board[tile.Coords.Item1, tile.Coords.Item2].Flip();
            }
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
            if (board[x, y] == null) return TileColor.BLANK;
            return board[x, y].color;
        }


        //find all possible plays given the current game state 
        // this takes into consideration whose turn it is
        public Dictionary<Tuple<int, int>, Play> PossiblePlays()
        {
            //for all open spots on the board that are adjacent to any tiles
            List<Tuple<int, int>> possiblePositions = board.OpenAdjacentSpots();
            Dictionary<Tuple<int, int>, Play> results = new Dictionary<Tuple<int, int>, Play>();

            TileColor playerColor = isPlayer1 ? TileColor.BLACK : TileColor.WHITE;

            foreach (Tuple<int,int> coord in possiblePositions)
            {

                Play possiblePlay = new Play(board, playerColor, coord);

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
            return board.BoardFull();
        }


    }
}
