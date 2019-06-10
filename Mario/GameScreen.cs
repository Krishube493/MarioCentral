using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Media;

namespace Mario
{
    public partial class GameScreen : UserControl
    {
        #region Global Variables

        //Lists
        List<Platform> Platforms = new List<Platform>();
        List<Enemies> Enemy = new List<Enemies>();
        List<PowerupBoxes> Powerups = new List<PowerupBoxes>();
        List<Ending> End = new List<Ending>();

        //Player One Controls 
        Boolean rightArrowDown, spaceDown, upArrowDown, leftArrowDown;
        public static int playerX = 20;
        public static int playerY = 275;
        public static int playerW = 40;
        public static int playerH = 55;

        public static int level = 1;
        public static int lives = 5;

        public int playerImage = 1;
        public int enemyImage = 1;

        //sounds
        SoundPlayer backgroundSound = new SoundPlayer(Properties.Resources.Background);
        SoundPlayer deathSound = new SoundPlayer(Properties.Resources.DeathSound);
        SoundPlayer jumpSound = new SoundPlayer(Properties.Resources.Jump);
        //player.Play();


        //jump controls 
        public int jumpNumber = 1;
        Boolean jumpActivated = false;

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            //Play sound
            backgroundSound.PlayLooping();
            LoadLevel();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {

            if (rightArrowDown == true && jumpActivated == false)
            {
                playerX += 10;
                PlayerImageChange();
            }

            if (upArrowDown == true || spaceDown == true)
            {
                Jump();
            }

            if (leftArrowDown == true && jumpActivated == false)
            {
                playerX -= 10;
                PlayerImageChange();
            }

            foreach (Enemies n in Enemy)
            {
                if (Enemies.EnemiesCollision(n) == "Right" || Enemies.EnemiesCollision(n) == "Left")
                {
                    lives--;
                    playerX = 20;
                    playerY = 275;
                }
                else if (Enemies.EnemiesCollision(n) == "Top")
                {
                    Enemy.Remove(n);
                    break;
                }
            }

            //powerups collision
            foreach (PowerupBoxes b in Powerups)
            {
                if (PowerupBoxes.PowerupsCollision(b) == true)
                {
                    lives++;
                    Powerups.Remove(b);
                    break;
                }
            }

            if (lives == 0)
            {
                //play sound 
                deathSound.Play();
                //go to game over screen 
                Reset();
                //Remove this screen 
                gameTimer.Enabled = false;
                Form f = this.FindForm();
                f.Controls.Remove(value: this);

                //Move Drectly to Game Screen 
                GameOverScreen os = new GameOverScreen();
                f.Controls.Add(os);

                //put into the middle of the screen
                os.Location = new Point((f.Width - os.Width) / 2, (f.Height - os.Height) / 2);
                os.Focus();
            }

            foreach (Ending q in End)
            {
                if (Ending.EndingCollision(q) == true)
                {
                    if (level == 1 || level == 2)
                    {
                        level += 1;
                        playerX = 20;
                        playerY = 275;
                        LoadLevel();
                    }
                    else
                    {
                        //go to win screen
                        Reset();
                        //stop sound 
                        backgroundSound.Stop();
                        //Remove this screen 
                        gameTimer.Enabled = false;
                        Form f = this.FindForm();
                        f.Controls.Remove(value: this);

                        //Move Drectly to Game Screen 
                        WinScreen ws = new WinScreen();
                        f.Controls.Add(ws);

                        //put into the middle of the screen
                        ws.Location = new Point((f.Width - ws.Width) / 2, (f.Height - ws.Height) / 2);
                        ws.Focus();
                    }
                }
            }

            EnemyImageChange();

            Refresh();
        }

        public void Jump()
        {
            if (jumpNumber == 1)
            {
                jumpSound.Play();
                playerY -= 20;
                playerX += 24;

                jumpActivated = true;
                jumpNumber = 2;
            }
            else if (jumpNumber == 2)
            {
                playerY -= 15;
                playerX += 19;

                jumpNumber = 3;
            }
            else if (jumpNumber == 3)
            {
                playerY -= 13;
                playerX += 17;

                jumpNumber = 4;
            }
            else if (jumpNumber == 4)
            {
                playerY -= 9;
                playerX += 13;

                jumpNumber = 5;
            }
            else if (jumpNumber == 5)
            {
                playerY += 9;
                playerX += 13;

                jumpNumber = 6;
            }
            else if (jumpNumber == 6)
            {
                playerY += 13;
                playerX += 17;

                jumpNumber = 7;
            }
            else if (jumpNumber == 7)
            {
                playerY += 15;
                playerX += 19;

                jumpNumber = 8;
            }
            else if (jumpNumber == 8)
            {
                playerY = 275;
                playerX += 24;

                //Play sound
                backgroundSound.PlayLooping();

                upArrowDown = false;
                spaceDown = false;
                jumpActivated = false;
                jumpNumber = 1;
            }
        }

        public void Reset()
        {
            playerX = 20;
            playerY = 275;
            playerW = 40;
            playerH = 55;
            level = 1;
            lives = 5;
            playerImage = 1;
            enemyImage = 1;
            jumpNumber = 1;
        }

        public void PlayerImageChange()
        {
            if (playerImage == 1)
            {
                playerImage = 2;
            }
            else if (playerImage == 2)
            {
                playerImage = 3;
            }
            else if (playerImage == 3)
            {
                playerImage = 4;
            }
            else if (playerImage == 4)
            {
                playerImage = 1;
            }
        }

        public void EnemyImageChange()
        {
            if (enemyImage == 1)
            {
                enemyImage = 2;

                foreach (Enemies n in Enemy)
                {
                    n.x += 6;
                }
            }
            else if (enemyImage == 2)
            {
                enemyImage = 3;

                foreach (Enemies n in Enemy)
                {
                    n.x += 6;
                }
            }
            else if (enemyImage == 3)
            {
                enemyImage = 4;

                foreach (Enemies n in Enemy)
                {
                    n.x -= 6;
                }
            }
            else if (enemyImage == 4)
            {
                enemyImage = 1;

                foreach (Enemies n in Enemy)
                {
                    n.x -= 6;
                }
            }
        }

        public void LoadLevel()
        {
            //XML File Thing
            Enemy.Clear();
            Platforms.Clear();
            Powerups.Clear();

            string NewName, NewX, NewY;
            NewName = NewX = NewY = " ";

            string fileName = "Resources/Level" + level + ".xml";
            XmlReader reader = XmlReader.Create(fileName);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    NewName = reader.ReadString();
                    reader.ReadToNextSibling("x");
                    NewX = reader.ReadString();
                    reader.ReadToNextSibling("y");
                    NewY = reader.ReadString();

                    if (NewName == "enemy")
                    {
                        Enemies n = new Enemies(Convert.ToInt32(NewX), Convert.ToInt32(NewY), 35);
                        Enemy.Add(n);
                    }
                    if (NewName == "platform")
                    {
                        Platform p = new Platform(Convert.ToInt32(NewX), Convert.ToInt32(NewY), 30);
                        Platforms.Add(p);
                    }
                    if (NewName == "powerup")
                    {
                        PowerupBoxes b = new PowerupBoxes(Convert.ToInt32(NewX), Convert.ToInt32(NewY), 30);
                        Powerups.Add(b);
                    }
                    if (NewName == "end")
                    {
                        Ending q = new Ending(Convert.ToInt32(NewX), Convert.ToInt32(NewY), 50);
                        End.Add(q);
                    }
                }

            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //create the ground 
            foreach (Platform p in Platforms)
            {
                e.Graphics.DrawImage(Properties.Resources.Platform, p.x, p.y, p.size, p.size);
            }

            //create the enemies 
            foreach (Enemies n in Enemy)
            {
                if (enemyImage == 1 || enemyImage == 2)
                {
                    e.Graphics.DrawImage(Properties.Resources.Enemy1, n.x, n.y, n.size, n.size);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.Enemy2, n.x, n.y, n.size, n.size);
                }
            }

            //create the ? boxes 
            foreach (PowerupBoxes b in Powerups)
            {
                e.Graphics.DrawImage(Properties.Resources.QuestionBox, b.x, b.y, b.size, b.size);
            }

            //create the player 
            if (playerImage == 1 || playerImage == 2)
            {
                e.Graphics.DrawImage(Properties.Resources.Mario1, playerX, playerY, playerW, playerH);
            }
            else
            {
                e.Graphics.DrawImage(Properties.Resources.Mario2, playerX, playerY, playerW, playerH);
            }

            //create ending
            foreach (Ending q in End)
            {
                e.Graphics.DrawImage(Properties.Resources.Tunnel, q.x, q.y, q.size, q.size);
            }

            livesLabel.Text = lives + " Lives";
        }
    }
}
