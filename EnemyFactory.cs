using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public static class EnemyFactory
    {
        public static BaseEnemy CreateEnemy(float x, float y, int type, int laneIndex, int dirY)
        {
            switch (type)
            {
                case 0: return new PoliceEnemy(x, y, 4, dirY, laneIndex);
                case 1: return new RedCarEnemy(x, y, 3, dirY, laneIndex);
                case 2: return new WhiteCarEnemy(x, y, 2, dirY, laneIndex);
                case 3: return new TaxiEnemy(x, y, 2, dirY, laneIndex);
                case 4: return new MotoEnemy(x, y, 3, dirY, laneIndex);
                default: return new PoliceEnemy(x, y, 4, dirY, laneIndex);
            }
        }
    }
}

