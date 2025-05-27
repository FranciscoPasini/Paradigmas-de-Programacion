using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyGame
{
    public class PoliceEnemy : BaseEnemy
    {
        // Constructor para el pool
        public PoliceEnemy() : base() { }

        public override void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            base.Initialize(posX, posY, speed, dirY, image, laneIndex);
            // Inicialización específica del enemigo policía
        }

        public override void Update()
        {
            base.Update();
            // Comportamiento específico del enemigo policía
        }

        public override void Reset()
        {
            base.Reset();
            // Reset específico del enemigo policía
        }
    }
}