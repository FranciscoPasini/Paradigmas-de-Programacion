using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : BasePlayer
    {
        public Player(float positionX, float positionY) : base(positionX, positionY)
        {
            playerController = new PlayerController(transform, 1); // Control específico
            CreateAnimations();
            renderer.SetTexture(idleDown.CurrentImage);
        }

        protected override void CreateAnimations()
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

            // Idle
            idleDown = new Animation("IdleDown", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Down/0.png") }, true);
            idleUp = new Animation("IdleUp", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Up/0.png") }, true);
            idleLeft = new Animation("IdleLeft", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Left/0.png") }, true);
            idleRight = new Animation("IdleRight", 0.1f, new List<Image> { Engine.LoadImage("assets/Player/Idle/Right/0.png") }, true);

            // Valor por defecto
            currentAnimation = runDown;
        }
        protected override void UpdateMovement(ref bool isMoving)
        {
            if (Engine.GetKey(Engine.KEY_W)) { currentAnimation = runUp; lastDirection = "Up"; isMoving = true; }
            if (Engine.GetKey(Engine.KEY_A)) { currentAnimation = runLeft; lastDirection = "Left"; isMoving = true; }
            if (Engine.GetKey(Engine.KEY_S)) { currentAnimation = runDown; lastDirection = "Down"; isMoving = true; }
            if (Engine.GetKey(Engine.KEY_D)) { currentAnimation = runRight; lastDirection = "Right"; isMoving = true; }
        }


    }
}