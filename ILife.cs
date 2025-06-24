using System;

namespace MyGame
{
    public interface ILife
    {
        int Lives { get; }
        void LoseLife();
        event EventHandler OnDeath;
    }
}