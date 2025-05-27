using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class WhiteCarEnemy : BaseEnemy
    {
        // Constructor para el pool
        public WhiteCarEnemy() : base() { }

        public override void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            base.Initialize(posX, posY, speed, dirY, image, laneIndex);
            // Inicialización específica del auto blanco
        }

        public override void Update()
        {
            base.Update();
            // Comportamiento específico del auto blanco
        }

        public override void Reset()
        {
            base.Reset();
            // Reset específico del auto blanco
        }
    }
}