using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemyMovement
    {
        private Transform transform;
        private int speed;
        private int dirY;

        public EnemyMovement(Transform transform, int speed, int dirY)
        {
            this.transform = transform;
            this.speed = speed;
            this.dirY = dirY;
        }

        public void Update()
        {
            if (transform != null)
            {
                transform.Translate(new Vector2(0, dirY), speed);
            }
        }

        public void Reset()
        {
            transform = null;
            speed = 0;
            dirY = 0;
        }
    }
}