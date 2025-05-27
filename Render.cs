using MyGame;

public class Renderer
{
    private Image texture;
    private int baseWidth;
    private int baseHeight;
    private Transform transform;

    public Renderer(Image texture, Vector2 baseSize, Transform transform)
    {
        this.texture = texture;
        this.baseWidth = (int)baseSize.x;
        this.baseHeight = (int)baseSize.y;
        this.transform = transform;
    }


    public void Draw()
    {
        Engine.Draw(texture, transform.Position.x, transform.Position.y);
    }

    public void SetTexture(Image newTexture)
    {
        this.texture = newTexture;
    }
}


