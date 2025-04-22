using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyGame;

public class Enemy
{
    // Sprites de enemigos hacia abajo
    private static Image policeDown = Engine.LoadImage("assets/Enemies/Police/PoliceD.png");
    private static Image redcarDown = Engine.LoadImage("assets/Enemies/RedCar/RedcarD.png");
    private static Image whitecarDown = Engine.LoadImage("assets/Enemies/WhiteCar/WhitecarD.png");
    private static Image taxiDown = Engine.LoadImage("assets/Enemies/Taxi/TaxiD.png");
    private static Image motoDown = Engine.LoadImage("assets/Enemies/Moto/MotoD.png");

    // Sprites de enemigos hacia arriba
    private static Image policeUp = Engine.LoadImage("assets/Enemies/Police/PoliceUp.png");
    private static Image redcarUp = Engine.LoadImage("assets/Enemies/RedCar/RedcarUp.png");
    private static Image whitecarUp = Engine.LoadImage("assets/Enemies/WhiteCar/WhitecarUp.png");
    private static Image taxiUp = Engine.LoadImage("assets/Enemies/Taxi/TaxiUp.png");
    private static Image motoUp = Engine.LoadImage("assets/Enemies/Moto/MotoUp.png");

    private Image enemyImage;
    private EnemyMovement enemyMovement;
    private Transform transform;

    public int laneIndex;

    public Transform Transform => transform;

    public Enemy(float positionX, float positionY, int type, int laneIndex)
    {
        transform = new Transform(new Vector2(positionX, positionY));

        int speed = 2;
        int dirY = (positionY < 0) ? 1 : -1;

        switch (type)
        {
            case 0:
                enemyImage = (dirY == 1) ? policeDown : policeUp;
                speed = 2;
                break;
            case 1:
                enemyImage = (dirY == 1) ? redcarDown : redcarUp;
                speed = 3;
                break;
            case 2:
                enemyImage = (dirY == 1) ? whitecarDown : whitecarUp;
                speed = 2;
                break;
            case 3:
                enemyImage = (dirY == 1) ? taxiDown : taxiUp;
                speed = 2;
                break;
            case 4:
                enemyImage = (dirY == 1) ? motoDown : motoUp;
                speed = 3;
                break;
        }

        enemyMovement = new EnemyMovement(transform, speed, dirY);
        this.laneIndex = laneIndex;
    }

    public void Update()
    {
        enemyMovement.Update();
    }

    public void Render()
    {
        Engine.Draw(enemyImage, transform.Position.x, transform.Position.y);
    }

    public bool IsOffScreen()
    {
        return transform.Position.y < -100 || transform.Position.y > 800;
    }
}
