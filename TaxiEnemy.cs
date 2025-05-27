using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class TaxiEnemy : BaseEnemy
    {
        // Constructor para el pool
        public TaxiEnemy() : base() { }

        public override void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            base.Initialize(posX, posY, speed, dirY, image, laneIndex);
            // Inicialización específica del taxi
        }

        public override void Update()
        {
            base.Update();
            // Comportamiento específico del taxi
        }

        public override void Reset()
        {
            base.Reset();
            // Reset específico del taxi
        }
    }
}