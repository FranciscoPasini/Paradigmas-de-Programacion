using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        private Image playerImage = Engine.LoadImage("assets/player.png");

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



        public Player(float positionX, float positionY)
        {
            transform = new Transform(new Vector2(positionX, positionY));
            playerController = new PlayerController(transform);
            CreateAnimations();
        }
        private void CreateAnimations()
        {
            // Run Down
            List<Image> downImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player/Run/Down/{i}.png");
                downImages.Add(image);
            }
            runDown = new Animation("RunDown", 0.1f, downImages, true);

            // Run Up
            List<Image> upImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player/Run/Up/{i}.png");
                upImages.Add(image);
            }
            runUp = new Animation("RunUp", 0.1f, upImages, true);

            // Run Left
            List<Image> leftImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player/Run/Left/{i}.png");
                leftImages.Add(image);
            }
            runLeft = new Animation("RunLeft", 0.1f, leftImages, true);

            // Run Right
            List<Image> rightImages = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                Image image = Engine.LoadImage($"assets/Player/Run/Right/{i}.png");
                rightImages.Add(image);
            }
            runRight = new Animation("RunRight", 0.1f, rightImages, true);

            // Set default
            currentAnimation = runDown;

            // Idle Down
            idleDown = new Animation("IdleDown", 0.1f, new List<Image> {Engine.LoadImage("assets/Player/Idle/Down/0.png") }, true);

            // Idle Up
            idleUp = new Animation("IdleUp", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Up/0.png")}, true);

            // Idle Left
            idleLeft = new Animation("IdleLeft", 0.1f, new List<Image> {  Engine.LoadImage("assets/Player/Idle/Left/0.png")}, true);

            // Idle Right
            idleRight = new Animation("IdleRight", 0.1f, new List<Image> {Engine.LoadImage("assets/Player/Idle/Right/0.png")}, true);

        }

        public delegate void CollisionEventHandler(object sender, EventArgs e);
        public event CollisionEventHandler OnCollision;

        public void Update()
        {

            bool isMoving = false;

            if (Engine.GetKey(Engine.KEY_W))
            {
                currentAnimation = runUp;
                lastDirection = "Up";
                isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_S))
            {
                currentAnimation = runDown;
                lastDirection = "Down";
                isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_A))
            {
                currentAnimation = runLeft;
                lastDirection = "Left";
                isMoving = true;
            }
            else if (Engine.GetKey(Engine.KEY_D))
            {
                currentAnimation = runRight;
                lastDirection = "Right";
                isMoving = true;
            }

            if (!isMoving)
            {
                switch (lastDirection)
                {
                    case "Up":
                        currentAnimation = idleUp;
                        break;
                    case "Down":
                        currentAnimation = idleDown;
                        break;
                    case "Left":
                        currentAnimation = idleLeft;
                        break;
                    case "Right":
                        currentAnimation = idleRight;
                        break;
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
                Enemy enemy = GameManager.Instance.LevelController.EnemyList[i];

                float DistanceX = Math.Abs((enemy.Transform.Position.x + enemy.Transform.Scale.x / 2) - (transform.Position.x + transform.Scale.x / 2));
                float DistanceY = Math.Abs((enemy.Transform.Position.y + enemy.Transform.Scale.y / 2) - (transform.Position.y + transform.Scale.y / 2));

                float sumHalfWidth = enemy.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float sumHalfHeight = enemy.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (DistanceX < sumHalfWidth && DistanceY < sumHalfHeight)
                {
                    OnCollision?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public void Render()
        {
            Engine.Draw(currentAnimation.CurrentImage, transform.Position.x, transform.Position.y);


        }
    }
}
