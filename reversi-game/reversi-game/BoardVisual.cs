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

namespace reversi_game
{
    public partial class BoardVisual : Form
    {
       
        private Game game;
        private DataGridView gameBoard;
        private const int BOARD_SIZE = 8;
        private Bitmap blank;
        private Bitmap black;
        private Bitmap white;
        private Bitmap hint;
        private int bitmapPadding = 6;

        Dictionary<Tuple<int, int>, Play> playable;

        public BoardVisual()
        {
            //TODO: Files loaded badly.  Must be in /bin already
            blank = (Bitmap)Image.FromFile("./green.bmp");
            black = (Bitmap)Image.FromFile("./black.bmp");
            white = (Bitmap)Image.FromFile("./white.bmp");
            hint = (Bitmap)Image.FromFile("./hint.bmp");


            game = new Game(BOARD_SIZE);

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

            PlaceFirstFour();

            playable = game.PossiblePlays();

        }
        //instead should just render the state of the board and place the first 4 elements in the Game or Board class
        private void PlaceFirstFour()
        {
            int x = (BOARD_SIZE - 1) / 2;
            int y = (BOARD_SIZE - 1) / 2;

            game.Place(x, y++);
            game.Place(x++, y);
            game.Place(x, y--);
            game.Place(x, y);

            playable = game.PossiblePlays();
            UpdateBoard();

        }

        #region setup grid
        private void ConfigureForm()
        {
            AutoSize = true;


            gameBoard = new System.Windows.Forms.DataGridView();
            gameBoard.Location = new Point(120, 0);
            gameBoard.AllowUserToAddRows = false;
            gameBoard.CellClick += new
                DataGridViewCellEventHandler(ClickCell);
            gameBoard.SelectionChanged += new
                EventHandler(Change_Selection);

            Controls.Add(gameBoard);
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
            while (columnCount < BOARD_SIZE);
        }

        private void CreateRows()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
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

            //if there was nothing, do nothing
            if(p != null)
            {
                //use the play
                game.UsePlay(p);

                if (game.GameOver())
                {
                    RenderGameOver();
                }
                else
                {
                    playable = game.PossiblePlays();
                }

                //rerender
                UpdateBoard();
            }            

        }

        //update a cell based on a new color at a position x,y
        private void RenderCell(int x, int y, TileColor color)
        {
            DataGridViewImageCell cell = (DataGridViewImageCell)
               gameBoard.Rows[y].Cells[x];
            cell.Value = color == TileColor.BLACK ? black : white;
        }

        private void UpdateBoard()
        {
            for(int x = 0; x < BOARD_SIZE; x++)
            {
                for(int y = 0; y < BOARD_SIZE; y++)
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

    }
}
