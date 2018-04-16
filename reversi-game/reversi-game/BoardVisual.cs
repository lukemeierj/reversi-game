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
        private int bitmapPadding = 6;


        public BoardVisual()
        {
            reversi_game.Properties.Resources test = Properties.Resources.MyImage.Green;
            Bitmap blank = (Bitmap)Image.FromFile("./green.bmp");
            Bitmap black = (Bitmap)Image.FromFile("./black.bmp");
            Bitmap white = (Bitmap)Image.FromFile("./white.bmp");

            game = new Game(BOARD_SIZE);

            InitializeComponent();

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
            ConfigureForm();
            SizeGrid();
            CreateColumns();
            CreateRows();

        }

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
            //gameBoard.CellMouseEnter += new
            //    DataGridViewCellEventHandler(dataGridView1_CellMouseEnter);
            //gameBoard.CellMouseLeave += new
            //    DataGridViewCellEventHandler(dataGridView1_CellMouseLeave);

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

        
        private void ClickCell(object sender, DataGridViewCellEventArgs e)
        {

           this.game.Place(e.ColumnIndex, e.RowIndex);
        }

    }
}
