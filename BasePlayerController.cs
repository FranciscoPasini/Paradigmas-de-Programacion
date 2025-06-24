using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class BasePlayerController : IController
    {
        protected Transform transform;
        protected int speed = 1;
        protected int playerId;

        public BasePlayerController(Transform transform, int playerId)
        {
            this.transform = transform;
            this.playerId = playerId;
        }

        protected abstract int GetKeyUp();
        protected abstract int GetKeyDown();
        protected abstract int GetKeyLeft();
        protected abstract int GetKeyRight();

        public void Update()
        {
            MoveUp();
            MoveDown();
            MoveLeft();
            MoveRight();
        }

        protected void MoveUp()
        {
            if (Engine.GetKey(GetKeyUp()))
            {
                transform.Translate(new Vector2(0, -1), speed);
                ClampToScreenTop();
            }
        }

        protected void MoveDown()
        {
            if (Engine.GetKey(GetKeyDown()))
            {
                transform.Translate(new Vector2(0, 1), speed);
                ClampToScreenBottom();
            }
        }

        protected void MoveLeft()
        {
            if (Engine.GetKey(GetKeyLeft()))
            {
                transform.Translate(new Vector2(-1, 0), speed);
                ClampToScreenLeft();
            }
        }

        protected void MoveRight()
        {
            if (Engine.GetKey(GetKeyRight()))
            {
                transform.Translate(new Vector2(1, 0), speed);
                CheckRightBoundary();
            }
        }

        private void ClampToScreenTop()
        {
            if (transform.Position.y <= 0)
                transform.Position = new Vector2(transform.Position.x, 0);
        }

        private void ClampToScreenBottom()
        {
            if (transform.Position.y >= 600)
                transform.Position = new Vector2(transform.Position.x, 600);
        }

        private void ClampToScreenLeft()
        {
            if (transform.Position.x <= 0)
                transform.Position = new Vector2(0, transform.Position.y);
        }

        private void CheckRightBoundary()
        {
            if (transform.Position.x >= 640)
            {
                transform.Position = new Vector2(80, transform.Position.y);
                GameManager.Instance.AddPoint(playerId);
            }
        }
    }
}
