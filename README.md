# Reversi (Othello) AI
#### By Luke Meier and Drew Hayward

## Summary
Here we implement an AI to play Reversi (better known as Othello).
This program includes four projects:
* **reversi-ui** - a Windows Forms interactive tool for human vs. human, computer vs. human, and computer vs. computer game play.  
* **reversi-game** - the underlying functionality of the Reversi game.  
* **reversi-ai** - the AI component of this project.  This utilizes MiniMax and optional alpha-beta pruning with various heuristics to search the Reversi game up to `n` ply.  
* **reversi-data** - driver code for testing and collecting data on computer vs. computer game play.  

## Reversi UI

This is a simple tool to play Reversi.  
* **Player vs. Player** - self-explanatory 
* **Computer vs. Computer** - watch two AI agents play against each other using the tile count heuristic.  Press next to advance moves.
* **Human vs. Computer** - Select *black* in order to play against *black*, and *white* to play against *white*
* **Stop/Clear** - creates a fresh game board.  

## Reversi Game
#### Game Manager
This class is the main interface with the Reversi game.  Ideally, you never need to go past this.
* Construct a new GameManager in one of three ways
	* For player vs. player game play, use `GameManager([uint size = 8])` 
	* For player vs. computer play, use `GameManager(heuristic, ply, opponentColor, [size = 8])`
		* This creates an AI to play for `opponentColor` using the heuristic `heuristic`, searching up to depth `ply`	.
	* For computer vs. computer play, use `GameManager(heuristic1, ply1, heuristic2, ply2, [size = 8])`
		* This creates two AI agents to play.  Black plays using `heuristic1` up to depth `ply1`, and white plays using `heuristic2` up to depth `ply2`.
* In order to take input from outside sources, use `gameManager.OutsidePlay(Play p)`.  This queues a play from an outside source, usually the reversi-ui project, but could be used to plug in other Reversi programs.  
	* You must play a valid move given the current game state.
* Advance one move forward in the game with `Game newState = gameManager.Next();`
* At any point, get the state of the current game with `gameManager.GetGame()`\
* At any point, reset the game with `gameManager.Rest()`
#### Game
This class implements a single instance of a Reversi game.
* Create a new game with `new Game([size=8])` or copy a game with `new Game(oldGame)`
* Use a play in a game with `game.UsePlay(play)`
* `game.GameOver()` returns whether or not a game either has a full board or is in deadlock
* `game.Size()` returns the size of the game board
* `game.PossiblePlays([otherPlayer = false])` returns a dictionary of possible plays, indexed by their coordinates on the board (i.e. `Dictionary<Tuple<int,int>,Play>`)
	* When `otherPlayer = true`, we calculate the moves possible if it were actually the opponent's turn.  
	* `game.ForkGame(play)` returns a new game with the given `play` already used on the new game.
	* `game.ColorAt(x,y)` gets the color tile at a given position on the board.  Possi`ble values for colors:
		* `TileColor.BLANK`
		* `TileColor.WHITE`
		* `TileColor.BLACK`
#### Play
This class keeps track of possible plays and the tiles the plays would effect. 
Plays contain 
* `TileColor game.Color`
* `Tuple<int, int> game.Coords` 
* `List<Tile> game.AffectedTiles`
* Create a `new Play(color, coordinates, tilesAffected)` or `new Play(color, coords)` and let the class calculate the affected tiles.  
## TODO: Document 
* Board class,
* Tile class
* Data project
* AI project 