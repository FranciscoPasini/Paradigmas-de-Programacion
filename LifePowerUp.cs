using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LifePowerUp : IPoolable
    {
        private Transform transform;
        private Renderer renderer;
        private bool active = false;
        private Animation animation;
        private float lifeTime = 10f;
        private float currentTime = 0f;
        private static Random random = new Random();

        public bool IsActive => active;
        public Transform Transform => transform;

        public LifePowerUp()
        {
            List<Image> frames = new List<Image>();
            for (int i = 0; i < 5; i++)
            {
                frames.Add(Engine.LoadImage($"assets/PowerUps/Life/{i}.png"));
            }

            animation = new Animation("PowerUp", 0.1f, frames, true);
            transform = new Transform(new Vector2(0, 0));
            renderer = new Renderer(null, new Vector2(50, 50), transform);
        }

        public void Update()
        {
            if (!active) return;

            currentTime += Time.DeltaTime;

            if (currentTime >= lifeTime)
            {
                Deactivate();
                return;
            }

            animation.Update();
            CheckPlayerCollisions();
        }

        private void CheckPlayerCollisions()
        {
            var player1 = GameManager.Instance.LevelController.Player1;
            var player2 = GameManager.Instance.LevelController.Player2;

            if (player1 != null && CheckCollisionWith(player1))
            {
                player1.AddLife();
                GameManager.Instance.LevelController.OnPowerUpCollected();
                Deactivate();
                return;
            }

            if (player2 != null && CheckCollisionWith(player2))
            {
                player2.AddLife();
                GameManager.Instance.LevelController.OnPowerUpCollected();
                Deactivate();
            }
        }

        public void Render()
        {
            if (!active) return;
            renderer.SetTexture(animation.CurrentImage);
            renderer.Draw();
        }

        public void Activate()
        {
            transform.Position = new Vector2(
                random.Next(100, 700),
                random.Next(100, 600));
            active = true;
            currentTime = 0f;
        }

        public void Deactivate()
        {
            active = false;
            GameManager.Instance.LevelController.ReturnLifePowerUp(this);
        }

        private bool CheckCollisionWith(BasePlayer player)
        {
            if (player == null) return false;

            return new Rect(
                transform.Position.x,
                transform.Position.y,
                transform.Scale.x,
                transform.Scale.y)
            .Overlaps(new Rect(
                player.Transform.Position.x,
                player.Transform.Position.y,
                player.Transform.Scale.x,
                player.Transform.Scale.y));
        }

        public void Reset()
        {
            active = false;
        }
    }

    public class Rect
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Overlaps(Rect other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }
    }
}