using Monosand;

namespace Test.AndroidGL;

public class MyGame : Core
{
    protected override void Initialize()
    {
        base.Initialize();
        //GraphicsDevice.Viewport = new(0, 0, 300, 300);
        Scene = new MyScene();
        Window.AllowUserResizing = true;
    }
}

public class MyScene : Scene
{
    public override void Begin()
    {
        base.Begin();
        AddEntity(new TextEntity("你好这是文字"));
        AddEntity(new Bg());
    }
}

public class TextEntity : Entity
{
    private string text;
    private SpriteFont font;

    public TextEntity(string text)
    {
        Position = new(100, 100);
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
        Core.CoreIns.SpriteBatch.DrawString(font, text, Position, Color.Black);
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
        Core.CoreIns.SpriteBatch.Draw(bgTex, Position, Color.White);
    }
}