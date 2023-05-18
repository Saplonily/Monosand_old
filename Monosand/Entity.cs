﻿namespace Monosand;

public class Entity
{
    public double Layer;
    public double Depth;
    public Vector2 Position;
    public Scene Scene { get; internal set; }
    public EntityComponentList Components { get; internal set; }

    public Entity()
        => Components = new(this);

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