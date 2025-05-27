using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class BaseEnemy : IPoolable
    {
        protected Renderer renderer;
        protected Transform transform;
        protected EnemyMovement movement;
        public int laneIndex;

        public Transform Transform => transform;
        public bool IsActive { get; private set; }

        protected BaseEnemy()
        {
            // Constructor vacío para el pool
        }

        public virtual void Initialize(float posX, float posY, int speed, int dirY, Image image, int laneIndex)
        {
            transform = new Transform(new Vector2(posX, posY));
            movement = new EnemyMovement(transform, speed, dirY);
            renderer = new Renderer(image, new Vector2(50, 50), transform);
            this.laneIndex = laneIndex;
            IsActive = true;
        }

        public virtual void Update() => movement?.Update();

        public virtual void Render() => renderer?.Draw();

        public virtual bool IsOffScreen()
        {
            return transform.Position.y < -100 || transform.Position.y > 800;
        }

        public virtual void Reset()
        {
            IsActive = false;
            transform = null;
            movement = null;
            renderer = null;
        }
    }
}