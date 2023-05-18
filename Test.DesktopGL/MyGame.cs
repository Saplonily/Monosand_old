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
        AddEntity(new TextEntity(new(100, 100), "你好这是文字最底层"));
        AddEntity(new TextEntity(new(100, 120), "你好这是文字第二层"));
        AddEntity(new TextEntity(new(100, 140), "你好这是文字最高层"));
        AddEntity(new TextEntity(new(100, 160), "你好这是文字比最高好高的层"));
        AddEntity(new Bg());
    }

    public override void Update()
    {
        base.Update();
        if (Input.IsKeyPressed(Keys.D))
        {
            Camera.Position += Vector2.UnitX;
        }
        if (Input.IsKeyPressed(Keys.W))
        {
            Camera.Position -= Vector2.UnitY;
        }
        if (Input.IsKeyPressed(Keys.S))
        {
            Camera.Position += Vector2.UnitY;
        }
        if (Input.IsKeyPressed(Keys.A))
        {
            Camera.Position -= Vector2.UnitX;
        }

        if(Input.IsKeyPressed(Keys.E))
        {
            Camera.Rotation += MathF.PI / 180;
        }

        if (Input.IsKeyPressed(Keys.Q))
        {
            Camera.Rotation -= MathF.PI / 180;
        }
    }
}

[Tracked]
public class TextEntity : Entity
{
    private string text;
    private SpriteFont font;

    public TextEntity(Vector2 position, string text)
    {
        Position = position;
        this.text = text;
        font = Core.CoreIns.Content.Load<SpriteFont>("font1");
    }

    public override void Update()
    {
        base.Update();
        //Position += Vector2.UnitX;
    }

    public override void Draw()
    {
        base.Draw();
        Core.CoreIns.SpriteBatch.DrawString(font, $"mpos: {Input.MousePosition}", Position, Color.White);
    }
}

public class Bg : Entity
{
    private Texture2D bgTex;

    public Bg()
    {
        bgTex = Core.CoreIns.Content.Load<Texture2D>("bg1");
    }

    public override void Draw()
    {
        base.Draw();
        Core.CoreIns.GraphicsDevice.Clear(Color.CornflowerBlue);
    }
}