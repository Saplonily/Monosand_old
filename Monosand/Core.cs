﻿namespace Monosand;

public class Core : Game
{
    private GameTime gameTime;
    private Scene scene;
    private Scene nextScene;
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;

    public static Core CoreIns { get; private set; }
    internal static DisplayMode DisplayMode => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
    public static Vector2 ScreenSize => new(DisplayMode.Width, DisplayMode.Height);
    public Vector2 WindowSize => new(
        GraphicsDevice.PresentationParameters.BackBufferWidth,
        GraphicsDevice.PresentationParameters.BackBufferHeight
        );
    public GameTime GameTime => gameTime;
    public Color? ClearColor { get; set; }
    public SpriteBatch SpriteBatch => spriteBatch;
    public Scene Scene { get => scene; set => nextScene = value; }
    public double Fps
    {
        get => 1d / gameTime.ElapsedGameTime.TotalSeconds;
        set => TargetElapsedTime = TimeSpan.FromSeconds(1d / value);
    }

    public Core()
    {
        CoreIns = this;
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        ClearColor = Color.CornflowerBlue;
    }

    public void PreferWindowSize(int width, int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        this.gameTime = gameTime;
        if (scene != nextScene)
        {
            scene?.End();
            nextScene?.Begin();
            scene = nextScene;
        }
        Input.UpdateWith(Keyboard.GetState(), Mouse.GetState());
        scene?.Update();
        scene?.UpdateDeferred();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (scene is not null)
        {
            GraphicsDevice.SetRenderTarget(scene.CameraRenderTarget);
            scene.Draw();
            GraphicsDevice.SetRenderTarget(null);
            SpriteBatch.Begin();
            SpriteBatch.Draw(scene.CameraRenderTarget, Vector2.Zero, Color.White);
            SpriteBatch.End();
        }
        base.Draw(gameTime);
    }
}
