namespace Monosand;

public class Component
{
    public Entity Entity { get; internal set; }
    public Scene Scene => Entity.Scene;

    public virtual void Update() { }

    public virtual void UpdateDeferred() { }

    public virtual void Draw() { }

    public virtual void EntityAwake() { }

    public virtual void Added()
    {
        Scene.Tracker.OnAdd(this);
    }

    public virtual void EntityAdded() { }

    public virtual void Removed() 
    {
        Scene.Tracker.OnRemove(this);
    }

    public virtual void EntityRemoved() { }

    public virtual void SceneBegin() { }

    public virtual void SceneEnd() { }
}