using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class MotoEnemy : BaseEnemy
    {
        public MotoEnemy(float x, float y, int speed, int dirY, int lane) : base(x, y, speed, dirY, dirY == 1 ? Engine.LoadImage("assets/Enemies/Moto/MotoD.png") : Engine.LoadImage("assets/Enemies/Moto/MotoUp.png"), lane)
        {

        }

    }
}
