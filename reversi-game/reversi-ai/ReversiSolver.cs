using System;
using System.Collections.Generic;
using System.Text;
using ReversiGame;
namespace ReversiAI
{
    class ReversiSolver
    {
        public ReversiSolver(Game game)
        {
            this.game = game;
            this.isPlayer1 = game.isPlayer1;
        }

        public Play ChoosePlay()
        {
            Dictionary<Tuple<int,int>, Play> possible = game.PossiblePlays();

            //look at the game as if we played a move for any given move
            foreach(KeyValuePair<Tuple<int,int>, Play> item in possible){
                Play play = item.Value;
                Game potentialGame = new Game(game);
                game.UsePlay(play);

            }
            return null;
        }

        private Game game;
        private bool isPlayer1;
    }
}
