using System;

namespace MyGame
{
    /// <summary>
    /// Interfaz para entidades con sistema de vidas.
    /// </summary>
    public interface ILife
    {
        int Lives { get; }
        void LoseLife();
        event EventHandler OnDeath;
    }
}