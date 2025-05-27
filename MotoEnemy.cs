using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class MotoEnemy : BaseEnemy
    {
        // Constructor para el pool
        public MotoEnemy() : base() { }

        public override void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            base.Initialize(posX, posY, speed, dirY, image, laneIndex);
            // Inicialización específica de la moto
        }

        public override void Update()
        {
            base.Update();
            // Comportamiento específico de la moto
        }

        public override void Reset()
        {
            base.Reset();
            // Reset específico de la moto
        }
    }
}