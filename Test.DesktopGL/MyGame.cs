using Monosand;

namespace Test.DesktopGL;

public class MyGame : Core
{
    protected override void Initialize()
    {
        base.Initialize();
        Window.AllowUserResizing = true;
        PreferWindowSize(1200, 800);
        var s = new MyScene();
        s.AddEntity(new TestEntity() { DepthLayer = -1 });
        s.AddEntity(new TestEntity() { Depth = -1, Position = Vector2.One * 15f });
        NextScene = s;
    }
}

public class TestEntity : Entity
{
    float angle;

    public TestEntity()
    {
        Size = new(400f, 40f);
    }

    public override void Draw()
    {
        base.Draw();
        angle += MathF.PI / 180;
        var s = Scene.Entities;
        Drawing.DrawRectangle(Position, Size, Color.AliceBlue);
        Drawing.DrawHollowRectangle(Position, Size, Color.Black);
        Drawing.DrawHollowRectangle(Position, new(600f, 60f), Color.CornflowerBlue);
        Drawing.DrawText(SceneAs<MyScene>().Font, $"d: {Depth}, l: {DepthLayer}", Bound.Center, TextAlign.Center, Color.CornflowerBlue);
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
        Camera.Position += dir;

        if (Input.IsKeyPressed(Keys.E))
        {
            Camera.Rotation += MathF.PI / 180;
        }

        if (Input.IsKeyPressed(Keys.Q))
        {
            Camera.Rotation -= MathF.PI / 180;
        }
    }
}