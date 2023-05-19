using Monosand;

namespace Test.DesktopGL;

public class MyGame : Core
{
    protected override void Initialize()
    {
        base.Initialize();
        Window.AllowUserResizing = true;
        PreferWindowSize(1200, 800);
        Scene = new MyScene();
    }
}

public class MyScene : Scene
{
    public SpriteFont Font;

    public override void Begin()
    {
        base.Begin();
        Font = Core.Asset.Load<SpriteFont>("font1");
    }

    public override void Update()
    {
        base.Update();
        Vector2 dir = new();
        if (Input.IsKeyPressed(Keys.D))
            dir += Vector2.UnitX;
        if (Input.IsKeyPressed(Keys.W))
            dir -= Vector2.UnitY;
        if (Input.IsKeyPressed(Keys.S))
            dir += Vector2.UnitY;
        if (Input.IsKeyPressed(Keys.A))
            dir -= Vector2.UnitX;
        Camera.Position += dir * 5;

        if (Input.IsKeyPressed(Keys.E))
        {
            Camera.Rotation += MathF.PI / 180;
        }

        if (Input.IsKeyPressed(Keys.Q))
        {
            Camera.Rotation -= MathF.PI / 180;
        }
    }

    public override void Draw()
    {
        base.Draw();
        Drawing.Batch.Begin(samplerState: CameraSamplerState, transformMatrix: Camera.InvertedMatrix);
        Drawing.DrawText(Font, $"a quick brown fox jumps over the lazy dog", Vector2.Zero, Color.White);
        Drawing.DrawLine(Camera.Bound.Size, Input.MousePosition, Color.White, 2f);
        Drawing.Batch.End();
    }
}