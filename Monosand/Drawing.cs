namespace Monosand;

public static class Drawing
{
    public static SpriteBatch Batch => Core.CoreIns.SpriteBatch;

    public static readonly Texture2D Pixel;

    static Drawing()
    {
        if (Core.CoreIns is null)
            throw new InvalidOperationException("Draw inited before Core inited");
        Pixel = new(Core.CoreIns.GraphicsDevice, 1, 1, false, SurfaceFormat.Color, 1);
        Pixel.SetData(new Color[] { Color.White });
    }

    public static void DrawTexture(Texture2D texture, Vector2 position)
        => Batch.Draw(texture, position, Color.White);

    public static void DrawPoint(Vector2 position, Color color)
        => Batch.Draw(Pixel, position, color);

    public static void DrawRectangle(Vector2 position, Vector2 size, Color color)
    {
        if (size.X < 0)
        {
            size.X = -size.X;
            position.X -= size.X;
        }
        if (size.Y < 0)
        {
            size.Y = -size.Y;
            position.Y -= size.Y;
        }
        Batch.Draw(Pixel, position, null, color, 0f, Vector2.Zero, size, SpriteEffects.None, 0f);
    }

    public static void DrawHollowRectangle(Vector2 position, Vector2 size, Color color, float thickness = 1f)
    {
        var leftTop = position;
        var rightTop = position + new Vector2(size.X, 0f);
        var leftBottom = position + new Vector2(0f, size.Y);
        var rightBottom = position + new Vector2(size.X, size.Y);
        DrawLine(leftTop, rightTop, color, thickness);
        DrawLine(rightTop, rightBottom, color, thickness);
        DrawLine(rightBottom, leftBottom, color, thickness);
        DrawLine(leftBottom, leftTop, color, thickness);
    }

    public static void DrawLine(Vector2 from, Vector2 to, Color color, float thickness = 1f)
    {
        Vector2 offset = to - from;
        float x = offset.X;
        float y = offset.Y;
        float rotation = MathF.Atan2(y, x);
        float length = offset.Length();
        from.Round();
        Batch.Draw(Pixel, from, null, color, rotation, Vector2.Zero, new Vector2(float.Round(length), thickness), SpriteEffects.None, 0f);
    }
}
