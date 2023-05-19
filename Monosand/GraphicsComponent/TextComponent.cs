namespace Monosand;

public class TextComponent : GraphicsComponent
{
    public SpriteFont Font;
    public string Text;

    public TextComponent(string text, SpriteFont font, Vector2 offset, Color color)
    {
        Font = font;
        Text = text;
        Offset = offset;
    }

    public override void Draw()
        => Drawing.DrawText(Font, Text, Entity.Position + Offset, Origin, Scale, Rotation, Color);
}