using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cursor.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Move Drectly to Game Screen 
            MenuScreen ms = new MenuScreen();
            this.Controls.Add(ms);

            //put into the middle of the screen
            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);
        }
    }
}
