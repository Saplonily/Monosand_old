namespace Monosand;

public class Entity
{
    internal float depth;
    internal float depthLayer;
    private Collider collider;
    public Vector2 Position;
    public Vector2 Size; /* only used for visibility check and bound */
    public Collider Collider { get => collider; set { collider = value; collider.Entity = this; } }

    public float DepthLayer { get => depthLayer; set { depthLayer = value; if (Scene is not null) Scene.Entities.dirty = true; } }
    public float Depth { get => depth; set { depth = value; if (Scene is not null) Scene.Entities.dirty = true; } }
    public Scene Scene { get; internal set; }
    public RectangleF Bound =>
        Collider is not null ? Collider.GetAbsoluteBound().UnitedWith(new(Position, Size)) : new(Position, Size);

    public EntityComponentList Components { get; internal set; }

    public Entity()
        => Components = new(this);


    public static int CompareByDepth(Entity entityA, Entity entityB)
    {
        int lc = -entityA.depthLayer.CompareTo(entityB.depthLayer);
        return lc != 0 ? lc : -entityA.depth.CompareTo(entityB.depth);
    }

    public T SceneAs<T>() where T : Scene
        => Scene as T;

    public void AddComponent(Component component)
        => Components.Add(component);

    public void RemoveComponent(Component component)
        => Components.Remove(component);

    public virtual void Update()
        => Components.Update();

    public virtual void UpdateDeferred()
        => Components.UpdateDeferred();

    public virtual void Draw()
        => Components.Draw();

    public virtual void Awake()
    {
        foreach (var com in Components)
            com.EntityAwake();
    }

    public virtual void Added()
    {
        foreach (var com in Components)
            com.EntityAdded();
        Scene.Tracker.OnAdd(this);
    }

    public virtual void Removed()
    {
        foreach (var com in Components)
            com.EntityRemoved();
        Scene.Tracker.OnRemove(this);
    }

    public virtual void SceneBegin()
    {
        foreach (var com in Components)
            com.SceneBegin();
    }

    public virtual void SceneEnd()
    {
        foreach (var com in Components)
            com.SceneEnd();
    }
}