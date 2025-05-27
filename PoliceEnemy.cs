using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PoliceEnemy : BaseEnemy
    {
        public PoliceEnemy(float x, float y, int speed, int dirY, int lane) : base(x, y, speed, dirY, dirY == 1 ? Engine.LoadImage("assets/Enemies/Police/PoliceD.png") : Engine.LoadImage("assets/Enemies/Police/PoliceUp.png"), lane)
        {

        }
        
    }
}
