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
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
            outputLabel.Text += " \n \n Right Arrow to Move Right \n Left Arrow to Move Left \n Up Arrow or Space to Jump Forward";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //Remove this screen 
            Form f = this.FindForm();
            f.Controls.Remove(value: this);

            //Move Drectly to Game Screen 
            GameScreen gs = new GameScreen();
            f.Controls.Add(gs);

            //put into the middle of the screen
            gs.Location = new Point((f.Width - gs.Width) / 2, (f.Height - gs.Height) / 2);
            gs.Focus();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ExitButton_Enter(object sender, EventArgs e)
        {
            exitButton.FlatAppearance.BorderSize = 3;
        }

        private void ExitButton_Leave(object sender, EventArgs e)
        {
            exitButton.FlatAppearance.BorderSize = 0;
        }

        private void StartButton_Enter(object sender, EventArgs e)
        {
            startButton.FlatAppearance.BorderSize = 3;
        }

        private void StartButton_Leave(object sender, EventArgs e)
        {
            startButton.FlatAppearance.BorderSize = 0;
        }
    }
}
