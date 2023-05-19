using Monosand;

namespace Test.DesktopGL;

public class MyGame : Core
{
    protected override void Initialize()
    {
        base.Initialize();
        Window.AllowUserResizing = true;
        PreferWindowSize(800, 600);
        Scene = new MyScene();
    }
}

public class MyScene : Scene
{
    public override void Begin()
    {
        base.Begin();
        Entity e = new();
        e.AddComponent(new TextComponent("你好这里是组件", Core.Asset.Load<SpriteFont>("font1"), Vector2.Zero, Color.White));
        e.AddComponent(new TextComponent("你好这里是组件2!", Core.Asset.Load<SpriteFont>("font1"), Vector2.UnitY * 20f, Color.White));
        AddEntity(e);
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
}