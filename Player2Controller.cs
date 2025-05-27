using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player2Controller : IController
    {
        private int speed = 1;
        private Transform tranform;

        public Player2Controller(Transform transform)
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
            if (Engine.GetKey(Engine.KEY_LEFT))
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
            if (Engine.GetKey(Engine.KEY_UP))
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
            if (Engine.GetKey(Engine.KEY_DOWN))
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
            if (Engine.GetKey(Engine.KEY_RIGHT))
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