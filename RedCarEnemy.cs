using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyGame
{
    public class RedCarEnemy : BaseEnemy
    {
        // Constructor para el pool
        public RedCarEnemy() : base() { }

        public override void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            base.Initialize(posX, posY, speed, dirY, image, laneIndex);
            // Inicialización específica del auto rojo
        }

        public override void Update()
        {
            base.Update();
            // Comportamiento específico del auto rojo
        }

        public override void Reset()
        {
            base.Reset();
            // Reset específico del auto rojo
        }
    }
}