using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class TaxiEnemy : BaseEnemy
    {
        public TaxiEnemy(float x, float y, int speed, int dirY, int lane) : base(x, y, speed, dirY, dirY == 1 ? Engine.LoadImage("assets/Enemies/Taxi/TaxiD.png") : Engine.LoadImage("assets/Enemies/Taxi/TaxiUp.png"), lane)
        {

        }

    }
}