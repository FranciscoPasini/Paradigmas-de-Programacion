using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerController : IController
    {
        private int speed = 1;
        private Transform tranform;

        public PlayerController(Transform transform)
        {
            this.tranform = transform;
        }

        public void Update()
        {
            MoveUp();
            MoveDown();
            MoveLeft();
            MoveRight();
        }

        public void MoveLeft()
        {
            if (Engine.GetKey(Engine.KEY_A))
            {
                tranform.Translate(new Vector2(-1, 0), speed);

                if (tranform.Position.x <= 0)
                {
                    tranform.Position = new Vector2(0, tranform.Position.y);
                }
            }
        }
        public void MoveUp()
        {
            if (Engine.GetKey(Engine.KEY_W))
            {
                tranform.Translate(new Vector2(0, -1), speed);

                if (tranform.Position.y <= 0)
                {
                    tranform.Position = new Vector2(tranform.Position.x, 0);
                }
            }
        }

        public void MoveDown()
        {
            if (Engine.GetKey(Engine.KEY_S))
            {
                tranform.Translate(new Vector2(0, 1), speed);

                if (tranform.Position.y >= 600)
                {
                    tranform.Position = new Vector2(tranform.Position.x, 600);
                }
            }
        }

        public void MoveRight()
        {
            if (Engine.GetKey(Engine.KEY_D))
            {
                tranform.Translate(new Vector2(1, 0), speed);
            }

            // Chequeo de colisión con borde derecho 
            if (tranform.Position.x >= 640)
            {
                tranform.Position = new Vector2(80, tranform.Position.y);
                GameManager.Instance.AddPoint(); // suma punto al volver al inicio
            }
        }
    }
}