using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mario
{
    class PowerupBoxes
    {
        public int x, y, size;

        public PowerupBoxes(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public static bool PowerupsCollision(PowerupBoxes b)
        {
            Rectangle rec1 = new Rectangle(GameScreen.playerX, GameScreen.playerY, GameScreen.playerW, GameScreen.playerH);
            Rectangle rec2 = new Rectangle(b.x, b.y, b.size, b.size);

            //check for collision 
            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
