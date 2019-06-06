using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mario
{
    class Enemies
    {
        public int x, y, size;

        public Enemies(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public static string EnemiesCollision(Enemies n)
        {
            Rectangle rec1 = new Rectangle(GameScreen.playerX, GameScreen.playerY, GameScreen.playerW, GameScreen.playerH);
            Rectangle rec2 = new Rectangle(n.x, n.y + n.size / 2 , 2, 2);
            Rectangle rec3 = new Rectangle(n.x + n.size, n.y + n.size / 2, 2, 2);
            Rectangle rec4 = new Rectangle(n.x + n.size/2, n.y, n.size/2, 2);

            //check for collision 
            if (rec1.IntersectsWith(rec4))
            {
                return "Top";
            }
            if (rec1.IntersectsWith(rec3))
            {
                return "Right";
            }
            if (rec1.IntersectsWith(rec2))
            {
                return "Left";
            }
            else 
            {
                return "none";
            }
        }
    }
}
