using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class GameScreen : UserControl
    {
        #region Global Variables

        //Lists
        List<Platform> Platforms = new List<Platform>();
        List<Enemies> Enemies = new List<Enemies>();
        List<PowerupBoxes> Powerups = new List<PowerupBoxes>();

        //Brushes

        //Player One Controls 
        Boolean leftArrowDown, rightArrowDown, spaceDown, upArrowDown, downArrowDown;
        public int lives = 5;

        #endregion
        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //where the game occurs 
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //create the ground 
            foreach (Platform p in Platforms)
            {

            }

            //create the enemies 

            //create the ? boxes 

            //create the player 
        }

    }
}
