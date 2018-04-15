using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reversi_game
{
    public partial class BoardVisual : Form
    {
        private const int BOARD_SIZE = 8;
        public BoardVisual()
        {

            Game game = new Game(BOARD_SIZE);

            InitializeComponent();
        }
    }
}
