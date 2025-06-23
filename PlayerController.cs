using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerController : BasePlayerController
    {
        public PlayerController(Transform transform, int playerId)
            : base(transform, playerId) { }

        protected override int GetKeyUp() => Engine.KEY_W;
        protected override int GetKeyDown() => Engine.KEY_S;
        protected override int GetKeyLeft() => Engine.KEY_A;
        protected override int GetKeyRight() => Engine.KEY_D;
    }
}