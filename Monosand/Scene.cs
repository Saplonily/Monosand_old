namespace Monosand;

public class Scene
{
    public RenderTarget2D CameraRenderTarget { get; internal set; }
    public SceneEntityList Entities { get; internal set; }
    public Camera Camera { get; set; }
    public Tracker Tracker { get; internal set; }

    public Scene()
    {
        Entities = new(this);
        Vector2 wSize = new(Core.CoreIns.WindowSize.X, Core.CoreIns.WindowSize.Y);
        Camera = new(wSize);
        CameraRenderTarget = new(Core.CoreIns.GraphicsDevice, (int)wSize.X, (int)wSize.Y);
        Tracker = new();
    }

    public void AddEntity(Entity entity)
        => Entities.Add(entity);

    public void RemoveEntity(Entity entity)
        => Entities.Remove(entity);

    public virtual void Update()
        => Entities.Update();

    public virtual void UpdateDeferred()
        => Entities.UpdateDeferred();

    public virtual void Draw()
    {
        var batch = Core.CoreIns.SpriteBatch;
        batch.Begin(transformMatrix: Matrix.Invert(Camera.Matrix));
        Entities.Draw();
        batch.End();
    }

    public virtual void Begin()
    {
        foreach (var entity in Entities)
            entity.SceneBegin();
    }

    public virtual void End()
    {
        foreach (var entity in Entities)
            entity.SceneEnd();
    }
}
