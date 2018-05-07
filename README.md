

# Reversi (Othello) AI
#### By Luke Meier and Drew Hayward

## Summary
Here we implement an AI to play Reversi (better known as Othello).
This program includes four projects:
* **reversi-ui** - a Windows Forms interactive tool for human vs. human, computer vs. human, and computer vs. computer game play.  
* **reversi-game** - the underlying functionality of the Reversi game.  
* **reversi-ai** - the AI component of this project.  This utilizes MiniMax and optional alpha-beta pruning with various heuristics to search the Reversi game up to `n` ply.  
* **reversi-data** - driver code for testing and collecting data on computer vs. computer game play.  

## Installation
* Ensure that `hint.bmp`, `green.bmp`, `black.bmp`, and `white.bmp` set to be copied to the output directory in VS. This setting is in properties of each individual image.  

## Reversi UI

This is a simple tool to play Reversi.  
* There are two sets of radio buttons.  One for the *black* heuristic and one for the *white*.  If the heuristic is anything other than *Human*, the computer will play with that heuristic on behalf of that tile color.  
* You can change the depth the solver goes to.  We recommend staying beneath 8 ply.  
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
#### Board
Manages the game's board as a `n x n` matrix of `Tile`s.
* Access `Tile` objects with board[x,y].
* Use `game.BoardFull()` to check if all the spots on a board are occupied.
* Use `game.Place(x,y)` to place a tile on the board.
* Use `new Board(size)` to create a new game board.
* Use `new Board(oldBoard)` to create a deep copy of a board.
#### Tile
`Tile` objects store their `Coords` and their `Color`
* Create with `new Tile(color)`
* Set coordinates with `tile.Place(x,y)`
	* This will return a `InvalidOperationException` if the tile was already placed.
* In order to change the color of a tile, call `tile.Flip()`
### Reversi AI
* **Heuristics**:  All heuristics take a `Game` object and a `TileColor` to optimize for.  
	* `TileCountHeuristic` -  calculated with `100*(MaxPlayerCount - MinPlayerCount)/(MaxPlayerCoins + MinPlayerCoins)`
	* `ActualMobilityHeuristic` - if there are moves for either player, `100*(MaxPlayerMobilityVal - MinPlayerMobilityVal)/(MaxPlayerMobilityVal + MinPlayerMobilityVal)`.  Otherwise, `0`.
   * `WeightedHeuristic` - the difference between the weighted value of all the times owned by the max player and all the weighted values owned by the min player.  NOTE:  Only works on 8x8 boards.
   * ` CornersHeuristic` - this is calculated as `100*(MaxPlayerCorners - MinPLayerCorners)/(MaxPlayerCorners + MinPlayerCorners)`.  If max and min together are `0`, return `0`
* To create an agent, `new ReversiSolver(color, heuristic, ply)`
* To get a play from the AI, call `var p = solver.ChoosePlay(game, [prune = true])`
	* This goes to the depth of `Ply`, as set in the constructor.  It only uses alpha beta pruning of prune is set to `true`
	* `game.SetHeuristic(heuristic)` sets the solver's heuristic to always optimize on behalf of it's own color.  That is, if the solver is *black*, the heuristic will score on behalf of *black*.
	*

### Reversi Data
This is driver code for testing.  It allows you to set most important things to compare and log the results.  
* `TestHeuristic(heuristic1, heuristic2, ply1, ply2, boardsize)` returns a tuple with the number of *black* and *white* tiles at the end of game play.   
