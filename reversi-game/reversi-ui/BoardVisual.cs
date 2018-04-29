using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ReversiGame;
using ReversiAI;

namespace ReversiUI
{
    public partial class BoardVisual : Form
    {

        enum GameMode
        {
            PvP,
            PvC,
            CvP,
            CvC
        }

        GameManager manager;
        Game game;
        private GameMode mode = GameMode.PvP;
        private DataGridView gameBoard;
        private const int BOARD_SIZE = 8;
        private Bitmap blank;
        private Bitmap black;
        private Bitmap white;
        private Bitmap hint;
        private int bitmapPadding = 6;
        private ReversiSolver s;

        Dictionary<Tuple<int, int>, Play> playable;

        public BoardVisual()
        {
            //TODO: Files loaded badly.  Must be in /bin already
            blank = (Bitmap)Image.FromFile("./green.bmp");
            black = (Bitmap)Image.FromFile("./black.bmp");
            white = (Bitmap)Image.FromFile("./white.bmp");
            hint = (Bitmap)Image.FromFile("./hint.bmp");


            manager = new GameManager(BOARD_SIZE);
            game = manager.GetGame();

            InitializeComponent();

            //add the data grid that has all the images 
            gameBoard = new DataGridView
            {
                BackColor = SystemColors.ButtonShadow,
                ForeColor = SystemColors.ControlText,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = new Size(855, 582),
                AutoSize = false,
                Name = "gameBoard",
            };

            gameBoard.AllowUserToAddRows = false;

            //mostly from online 
            ConfigureForm();
            SizeGrid();
            CreateColumns();
            CreateRows();


            playable = game.PossiblePlays();

            UpdateBoard();

        }


        #region setup grid
        private void ConfigureForm()
        {
            AutoSize = true;

            gameBoard.AllowUserToAddRows = false;
            gameBoard.CellClick += new
                DataGridViewCellEventHandler(ClickCell);
            gameBoard.SelectionChanged += new
                EventHandler(Change_Selection);

            GamePanel.Controls.Add(gameBoard);
        }


        private void SizeGrid()
        {
            gameBoard.ColumnHeadersVisible = false;
            gameBoard.RowHeadersVisible = false;
            gameBoard.AllowUserToResizeColumns = false; ;
            gameBoard.AllowUserToResizeRows = false;
            gameBoard.BorderStyle = BorderStyle.None;

            //Add twice the padding for the top of the cell 
            //and the bottom.
            gameBoard.RowTemplate.Height = blank.Height +
                2 * bitmapPadding + 1;

            gameBoard.AutoSize = true;
        }


        private void CreateColumns()
        {
            DataGridViewImageColumn imageColumn;
            int columnCount = 0;
            do
            {
                Bitmap unMarked = blank;
                imageColumn = new DataGridViewImageColumn();

                //Add twice the padding for the left and 
                //right sides of the cell.
                imageColumn.Width = blank.Width + 2 * bitmapPadding + 1;

                imageColumn.Image = unMarked;
                gameBoard.Columns.Add(imageColumn);
                columnCount = columnCount + 1;
            }
            while (columnCount < game.Size());
        }

        private void CreateRows()
        {
            for (int i = 0; i < game.Size(); i++)
            {
                gameBoard.Rows.Add();
            }
        }

        private void Change_Selection(object sender, EventArgs e)
        {
            this.gameBoard.ClearSelection();
        }
        #endregion

        private void RenderGameOver()
        {
            //dummy
        }

        //whenever we click on a cell
        private void ClickCell(object sender, DataGridViewCellEventArgs e)
        {
            Tuple<int, int> destCoords = Tuple.Create(e.ColumnIndex, e.RowIndex);

            //if there exists a valid play at this coordinate, get object
            playable.TryGetValue(destCoords, out Play p);
            Play humanPlay = manager.HumanPlay(p);
            if (humanPlay == null) return;
            Game next = manager.Next();
            if(next != null)
            {
                game = next;
            }
            else
            {
                throw new ArgumentException("No human player/other game manager error");
            }
            playable = game.PossiblePlays();
            UpdateBoard();
        }

        //set gameboard view to represent the game's board state
        private void UpdateBoard()
        {
            for (int x = 0; x < game.Size(); x++)
            {
                for(int y = 0; y < game.Size(); y++)
                {
                    DataGridViewImageCell cell = (DataGridViewImageCell)gameBoard.Rows[y].Cells[x];
                    switch (game.ColorAt(x, y))
                    {
                        case TileColor.BLACK:
                            cell.Value = black;
                            break;
                        case TileColor.WHITE:
                            cell.Value = white;
                            break;
                        default:
                            //if a place is playable, show as a hint
                            if (playable != null && playable.ContainsKey(Tuple.Create(x, y)))
                            {
                                cell.Value = hint;
                            } else
                            {
                                cell.Value = blank;
                            }
                            break;
                    }
                }
            }
        }

        private void ChangeGameMode(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            switch (c.Tag)
            {
                case "PvP":
                    if (mode == GameMode.PvP) break;
                    mode = GameMode.PvP;
                    manager = new GameManager();
                    break;
                case "PvC":
                    if (mode == GameMode.PvC) break;
                    mode = GameMode.PvC;
                    manager = new GameManager(ReversiSolver.BasicHeuristic, 5, TileColor.WHITE);
                    break;
                case "CvP":
                    if (mode == GameMode.CvP) break;
                    mode = GameMode.CvP;
                    manager = new GameManager(ReversiSolver.BasicHeuristic, 5, TileColor.BLACK);
                    break;
                case "CvC":
                    if (mode == GameMode.CvC) break;
                    mode = GameMode.CvC;
                    manager = new GameManager(ReversiSolver.BasicHeuristic, 5, ReversiSolver.ActualMobilityHeuristic, 5);
                    break;
            }
            game = manager.GetGame();
            playable = game.PossiblePlays();
            UpdateBoard();
        }



        private void NextMove(object sender, EventArgs e)
        {
            if (game.Winner != null) return;
            Game next = manager.Next();
            if (next != null) game = next;
            playable = game.PossiblePlays();
            UpdateBoard();

        }

        private void Reset(object sender, EventArgs e)
        {
            manager.Reset();
            game = manager.GetGame();
            UpdateBoard();
        }
    }
}
