using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class RedCarEnemy : BaseEnemy
    {
        public RedCarEnemy(float x, float y, int speed, int dirY, int lane) : base(x, y, speed, dirY, dirY == 1 ? Engine.LoadImage("assets/Enemies/RedCar/RedCarD.png") : Engine.LoadImage("assets/Enemies/RedCar/RedCarUp.png"), lane)
        {

        }

    }
}
