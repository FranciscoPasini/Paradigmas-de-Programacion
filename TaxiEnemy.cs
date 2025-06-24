using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class TaxiEnemy : BaseEnemy
    {
        public TaxiEnemy() : base() { }

        public override void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            base.Initialize(posX, posY, speed, dirY, image, laneIndex);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}