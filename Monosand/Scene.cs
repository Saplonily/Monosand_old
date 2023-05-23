namespace Monosand;

public class Scene
{
    public RenderTarget2D CameraRenderTarget { get; internal set; }
    public SceneEntityList Entities { get; internal set; }
    public Camera Camera { get; set; }
    public SamplerState CameraSamplerState { get; set; }
    public Tracker Tracker { get; internal set; }
    public Vector2 CameraCullingPadding { get; set; }
    public Comparison<Entity> EntitySorting { get; }

    public Scene() : this(Entity.CompareByDepth) { }

    public Scene(Comparison<Entity> entitySorting)
    {
        Vector2 wSize = new(Core.CoreIns.WindowSize.X, Core.CoreIns.WindowSize.Y);
        CameraRenderTarget = new(Core.CoreIns.GraphicsDevice, (int)wSize.X, (int)wSize.Y);
        CameraSamplerState = Core.CoreIns.DefaultCameraSamplerState;
        CameraCullingPadding = new Vector2(64, 64);
        Camera = new(wSize);
        Tracker = new();
        Entities = new(this);
        EntitySorting = entitySorting;
    }

    public void AddEntity(Entity entity)
        => Entities.Add(entity);

    public void RemoveEntity(Entity entity)
        => Entities.Remove(entity);

    public virtual void Update()
        => Entities.Update();

    public virtual void UpdateDeferred()
        => Entities.UpdateDeferred();

    public virtual void BeginSceneDrawBatch()
        => Drawing.Batch.Begin(
            transformMatrix: Camera.InvertedMatrix,
            samplerState: CameraSamplerState,
            rasterizerState: RasterizerState.CullNone
            );

    public virtual void EndSceneDrawBatch()
        => Drawing.Batch.End();

    public virtual void BeginSceneShapeBatch()
        => Drawing.ShapeBatch.Begin(
            matrix: Camera.InvertedMatrix,
            samplerState: CameraSamplerState,
            rasterizerState: RasterizerState.CullNone
            );

    public virtual void EndSceneShapeBatch()
        => Drawing.ShapeBatch.End();

    public virtual void Draw()
    {
        BeginSceneDrawBatch();
        Entities.Draw();
        EndSceneDrawBatch();
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
