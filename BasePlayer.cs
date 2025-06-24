using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class BasePlayer : ILife
    {
        // --- Campos comunes ---
        protected readonly Vector2 startPosition;
        protected Transform transform;
        protected IController playerController; // Se asignará en las clases hijas
        protected Renderer renderer;

        // Animaciones (protegidas para acceso en hijas)
        protected Animation runDown, runUp, runLeft, runRight;
        protected Animation idleDown, idleUp, idleLeft, idleRight;
        protected Animation currentAnimation;
        protected string lastDirection = "Down";

        // ILife
        protected int lives = 3;
        public int Lives => lives;
        public event EventHandler OnDeath;

        public Transform Transform => transform;
        public bool IsActive { get; protected set; } = true;

        // Constructor base
        public BasePlayer(float positionX, float positionY)
        {
            startPosition = new Vector2(positionX, positionY);
            transform = new Transform(new Vector2(positionX, positionY));
            renderer = new Renderer(null, new Vector2(50, 50), transform); // Texture se seteará en hijas
        }

        // Método abstracto para cargar animaciones (cada jugador tiene rutas diferentes)
        protected abstract void CreateAnimations();

        // --- Lógica común ---
        public virtual void Update()
        {
            bool isMoving = false;
            UpdateMovement(ref isMoving); // Método para manejar inputs (sobrescribible)

            if (!isMoving)
            {
                UpdateIdleAnimation();
            }

            playerController.Update();
            currentAnimation.Update();
            CheckCollisions();
        }

        protected virtual void UpdateMovement(ref bool isMoving)
        {
            // Implementación vacía (las hijas definirán los controles)
        }

        protected void UpdateIdleAnimation()
        {
            switch (lastDirection)
            {
                case "Up": currentAnimation = idleUp; break;
                case "Down": currentAnimation = idleDown; break;
                case "Left": currentAnimation = idleLeft; break;
                case "Right": currentAnimation = idleRight; break;
            }
        }

        private void CheckCollisions()
        {
            foreach (var enemy in GameManager.Instance.LevelController.EnemyList)
            {
                float dx = Math.Abs((enemy.Transform.Position.x + enemy.Transform.Scale.x / 2)
                                  - (transform.Position.x + transform.Scale.x / 2));
                float dy = Math.Abs((enemy.Transform.Position.y + enemy.Transform.Scale.y / 2)
                                  - (transform.Position.y + transform.Scale.y / 2));

                if (dx < (enemy.Transform.Scale.x + transform.Scale.x) / 2 &&
                    dy < (enemy.Transform.Scale.y + transform.Scale.y) / 2)
                {
                    LoseLife();
                    break;
                }
            }
        }

        public void Render()
        {
            renderer.SetTexture(currentAnimation.CurrentImage);
            renderer.Draw();
        }

        // ILife implementation
        public void LoseLife()
        {
            if (lives <= 0) return;
            lives--;
            if (lives == 0)
                OnDeath?.Invoke(this, EventArgs.Empty);
            else
                transform.Position = startPosition; // Respawn
        }
        public void AddLife()
        {
            lives++;
        }

    }
}