namespace Monosand;

public abstract class GraphicsComponent : Component
{
    public Vector2 Origin;
    public Vector2 Offset;
    public Vector2 Scale;
    public Color Color;
    public float Rotation;

    public GraphicsComponent()
    {
        Origin = Vector2.Zero;
        Offset = Vector2.Zero;
        Scale = Vector2.One;
        Color = Color.White;
        Rotation = 0f;
    }
}