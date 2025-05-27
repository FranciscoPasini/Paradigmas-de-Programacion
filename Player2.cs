using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player2 : ILife
    {
        private readonly Vector2 startPosition;    // <--- posición de respawn
        private IController player2Controller;
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

        public Player2(float positionX, float positionY)
        {
            startPosition = new Vector2(positionX, positionY);  // <--- grabamos la posición inicial
            transform = new Transform(new Vector2(positionX, positionY));
            player2Controller = new Player2Controller(transform);
            CreateAnimations();
            renderer = new Renderer(idleDown.CurrentImage, new Vector2(50, 50), transform);
        }

        private void CreateAnimations()
        {
            // Run Down
            List<Image> downImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player2/Run/Down/{i}.png");
                downImages.Add(image);
            }
            runDown = new Animation("RunDown", 0.1f, downImages, true);

            // Run Up
            List<Image> upImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player2/Run/Up/{i}.png");
                upImages.Add(image);
            }
            runUp = new Animation("RunUp", 0.1f, upImages, true);

            // Run Left
            List<Image> leftImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player2/Run/Left/{i}.png");
                leftImages.Add(image);
            }
            runLeft = new Animation("RunLeft", 0.1f, leftImages, true);

            // Run Right
            List<Image> rightImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player2/Run/Right/{i}.png");
                rightImages.Add(image);
            }
            runRight = new Animation("RunRight", 0.1f, rightImages, true);

            // Idle
            idleDown = new Animation("IdleDown", 0.1f, new List<Image> { Engine.LoadImage("assets/Player2/Idle/Down/0.png") }, true);
            idleUp = new Animation("IdleUp", 0.1f, new List<Image> { Engine.LoadImage("assets/Player2/Idle/Up/0.png") }, true);
            idleLeft = new Animation("IdleLeft", 0.1f, new List<Image> { Engine.LoadImage("assets/Player2/Idle/Left/0.png") }, true);
            idleRight = new Animation("IdleRight", 0.1f, new List<Image> { Engine.LoadImage("assets/Player2/Idle/Right/0.png") }, true);

            // Valor por defecto
            currentAnimation = runDown;
        }

        public delegate void CollisionEventHandler(object sender, EventArgs e);
        public event CollisionEventHandler OnCollision;

        public void Update()
        {
            bool isMoving = false;

            if (Engine.GetKey(Engine.KEY_UP))
            {
                currentAnimation = runUp; lastDirection = "Up"; isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_DOWN))
            {
                currentAnimation = runDown; lastDirection = "Down"; isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_LEFT))
            {
                currentAnimation = runLeft; lastDirection = "Left"; isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_RIGHT))
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

            player2Controller.Update();
            currentAnimation.Update();
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < GameManager.Instance.LevelController.EnemyList.Count; i++)
            {
                BaseEnemy enemy = GameManager.Instance.LevelController.EnemyList[i];
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