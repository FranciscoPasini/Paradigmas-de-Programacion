using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : ILife
    {
        private readonly Vector2 startPosition;    // posición de respawn
        private IController playerController;
        private Transform transform;

        private Animation runDown;
        private Animation runUp;
        private Animation runLeft;
        private Animation runRight;
        private Animation currentAnimation;
        private Animation idleDown;
        private Animation idleUp;
        private Animation idleLeft;
        private Animation idleRight;

        private string lastDirection = "Down";
        private readonly Renderer renderer;

        // ILife
        private int lives = 3;
        public int Lives => lives;
        public event EventHandler OnDeath;

        public Player(float positionX, float positionY)
        {
            startPosition = new Vector2(positionX, positionY);  // guardamos el punto de inicio
            transform = new Transform(new Vector2(positionX, positionY));
            playerController = new PlayerController(transform, 1);
            CreateAnimations();
            renderer = new Renderer(idleDown.CurrentImage, new Vector2(50, 50), transform);
        }

        private void CreateAnimations()
        {
            // Run Down
            var downImages = new List<Image>();
            for (int i = 0; i < 3; i++)
                downImages.Add(Engine.LoadImage($"assets/Player/Run/Down/{i}.png"));
            runDown = new Animation("RunDown", 0.1f, downImages, true);

            // Run Up
            var upImages = new List<Image>();
            for (int i = 0; i < 3; i++)
                upImages.Add(Engine.LoadImage($"assets/Player/Run/Up/{i}.png"));
            runUp = new Animation("RunUp", 0.1f, upImages, true);

            // Run Left
            var leftImages = new List<Image>();
            for (int i = 0; i < 3; i++)
                leftImages.Add(Engine.LoadImage($"assets/Player/Run/Left/{i}.png"));
            runLeft = new Animation("RunLeft", 0.1f, leftImages, true);

            // Run Right
            var rightImages = new List<Image>();
            for (int i = 0; i < 3; i++)
                rightImages.Add(Engine.LoadImage($"assets/Player/Run/Right/{i}.png"));
            runRight = new Animation("RunRight", 0.1f, rightImages, true);

            // Idle
            idleDown = new Animation("IdleDown", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Down/0.png") }, true);
            idleUp = new Animation("IdleUp", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Up/0.png") }, true);
            idleLeft = new Animation("IdleLeft", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Left/0.png") }, true);
            idleRight = new Animation("IdleRight", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Right/0.png") }, true);

            currentAnimation = runDown;
        }

        public void Update()
        {
            bool isMoving = false;
            if (Engine.GetKey(Engine.KEY_W))
            {
                currentAnimation = runUp; lastDirection = "Up"; isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_S))
            {
                currentAnimation = runDown; lastDirection = "Down"; isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_A))
            {
                currentAnimation = runLeft; lastDirection = "Left"; isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_D))
            {
                currentAnimation = runRight; lastDirection = "Right"; isMoving = true;
            }

            if (!isMoving)
            {
                switch (lastDirection)
                {
                    case "Up": currentAnimation = idleUp; break;
                    case "Down": currentAnimation = idleDown; break;
                    case "Left": currentAnimation = idleLeft; break;
                    case "Right": currentAnimation = idleRight; break;
                }
            }

            playerController.Update();
            currentAnimation.Update();
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < GameManager.Instance.LevelController.EnemyList.Count; i++)
            {
                var enemy = GameManager.Instance.LevelController.EnemyList[i];
                float dx = Math.Abs((enemy.Transform.Position.x + enemy.Transform.Scale.x / 2)
                                  - (transform.Position.x + transform.Scale.x / 2));
                float dy = Math.Abs((enemy.Transform.Position.y + enemy.Transform.Scale.y / 2)
                                  - (transform.Position.y + transform.Scale.y / 2));

                float halfW = enemy.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float halfH = enemy.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (dx < halfW && dy < halfH)
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
                transform.Position = startPosition;  // respawn
        }
    }
}