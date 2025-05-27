using MyGame;

public class EnemyMovement
{
    private Transform transform;
    private int speed;
    private int dirY;

    public EnemyMovement(Transform transform, int speed, int dirY)
    {
        this.transform = transform;
        this.speed = speed;
        this.dirY = dirY;
    }

    public void Update()
    {
        transform.Translate(new Vector2(0, dirY), speed);
    }
}
