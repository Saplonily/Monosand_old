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
        NextScene = s;
    }
}

public class Player : Entity
{
    public Vector2 Speed;

    public Player()
    {
        Size = Vector2.One * 30f;
        Collider = new BoxCollider(30f);
    }

    public override void Awake()
    {
        base.Awake();
        Position = Scene.Camera.Bound.Center - Bound.Size / 2;
    }

    public override void Update()
    {
        base.Update();
        Vector2 dir = Vector2.Zero;
        if (Input.IsKeyPressed(Keys.A))
            dir -= Vector2.UnitX;
        if (Input.IsKeyPressed(Keys.D))
            dir += Vector2.UnitX;
        if (Input.IsKeyPressed(Keys.W))
            dir -= Vector2.UnitY;
        if (Input.IsKeyPressed(Keys.S))
            dir += Vector2.UnitY;
        dir = dir.SafeNormalized();
        Speed = MathS.Approach(Speed, Vector2.Zero, 500f * Core.DeltaF);
        Speed = MathS.Approach(Speed, dir * 500f, 4000f * Core.DeltaF);
        Vector2 val = Speed * Core.DeltaF;
        int xSign = Math.Sign(val.X);
        int ySign = Math.Sign(val.Y);
        while (xSign == 1 ? val.X > 0f : val.X < 0f)
        {
            Position.X += xSign;
            val.X -= xSign;
            if (Collider.CollideAny<Obstacle>())
            {
                Position.X -= xSign;
                break;
            }
        }
        while (ySign == 1 ? val.Y > 0f : val.Y < 0f)
        {
            Position.Y += ySign;
            val.Y -= ySign;
            if (Collider.CollideAny<Obstacle>())
            {
                Position.Y -= ySign;
                break;
            }
        }

        if (Input.IsKeyJustPressed(Keys.C))
        {
            foreach (var c in Collider.CollideAll<Obstacle>())
            {
                c.RemoveSelf();
            }
        }
    }

    public override void Draw()
    {
        base.Draw();
        Drawing.DrawHollowRectangle(Position, Size, Color.White, 5f);
    }
}

[Tracked]
public class Obstacle : Entity
{
    public Obstacle()
    {
        Collider = new BoxCollider(10f);
    }

    public override void Draw()
    {
        base.Draw();
        Drawing.DrawRectangle(Position, Vector2.One * 10f, Color.CornflowerBlue);
    }
}

public class MyScene : Scene
{
    public SpriteFont Font;

    public override void Begin()
    {
        base.Begin();
        Font = Core.Asset.Load<SpriteFont>("font1");
        AddEntity(new Player());
        for (int i = 0; i < 200; i++)
        {
            Obstacle o = new();
            Vector2 max = Camera.Bound.Size - o.Bound.Size;
            o.Position = Random.Shared.NextVector2(max);
            AddEntity(o);
        }
    }
}