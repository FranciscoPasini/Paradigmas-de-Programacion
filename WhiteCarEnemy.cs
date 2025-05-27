using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class WhiteCarEnemy : BaseEnemy
    {
        public WhiteCarEnemy(float x, float y, int speed, int dirY, int lane) : base(x, y, 2, dirY, dirY == 1 ? Engine.LoadImage("assets/Enemies/WhiteCar/WhiteCarD.png") : Engine.LoadImage("assets/Enemies/WhiteCar/WhiteCarUp.png"), lane)
        {

        }

    }
}
