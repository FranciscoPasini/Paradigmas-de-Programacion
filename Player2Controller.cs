using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player2Controller : BasePlayerController
    {
        public Player2Controller(Transform transform)
            : base(transform, 2) { } // PlayerId fijo = 2

        protected override int GetKeyUp() => Engine.KEY_UP;
        protected override int GetKeyDown() => Engine.KEY_DOWN;
        protected override int GetKeyLeft() => Engine.KEY_LEFT;
        protected override int GetKeyRight() => Engine.KEY_RIGHT;
    }
}