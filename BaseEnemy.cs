using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class BaseEnemy
    {
        protected Image enemyImage;
        protected Transform transform;
        protected EnemyMovement movement;
        public int laneIndex;

        public Transform Transform => transform;

        public BaseEnemy(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            transform = new Transform(new Vector2(posX, posY));
            movement = new EnemyMovement(transform, speed, dirY);
            enemyImage = image;
            this.laneIndex = laneIndex;
        }

        public virtual void Update()
        {
            movement.Update();
        }

        public virtual void Render()
        {
            Engine.Draw(enemyImage, transform.Position.x, transform.Position.y);
        }

        public virtual bool IsOffScreen()
        {
            return transform.Position.y < -100 || transform.Position.y > 800;
        }
    }
}
