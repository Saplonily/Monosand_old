namespace Monosand;

public class Image : GraphicsComponent
{
    public readonly Texture2D Texture;

    public Image(Texture2D texture) : this(texture, Vector2.Zero) { }

    public Image(Texture2D texture, Vector2 offset)
    {
        Offset = offset;
        Texture = texture;
    }

    public void MakeCenter()
        => Origin = Texture.Bounds.Size.ToVector2() / 2f;

    public override void Draw()
        => Drawing.DrawTexture(Texture, Entity.Position + Offset, Scale, Origin, Rotation);

}