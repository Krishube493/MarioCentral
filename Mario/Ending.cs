using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mario
{
    class Ending
    {
        public int x, y, size;

        public Ending(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public static bool EndingCollision(Ending q)
        {
            Rectangle rec1 = new Rectangle(GameScreen.playerX, GameScreen.playerY, GameScreen.playerW, GameScreen.playerH);
            Rectangle rec2 = new Rectangle(q.x, q.y, q.size, q.size);

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
