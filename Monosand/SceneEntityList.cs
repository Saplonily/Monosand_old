using System.Collections;

namespace Monosand;

public sealed class SceneEntityList : IReadOnlyCollection<Entity>
{
    public Scene Scene { get; }
    private readonly List<Entity> current;
    private readonly List<Entity> pendingAdd;
    private readonly List<Entity> pendingRemove;
    private readonly List<Entity> pendingAwake;

    public SceneEntityList(Scene belongTo)
    {
        Scene = belongTo;
        current = new(8);
        pendingAdd = new(4);
        pendingAwake = new(4);
        pendingRemove = new(2);
    }

    public void Add(Entity entity)
        => pendingAdd.Add(entity);

    public void Remove(Entity entity)
        => pendingRemove.Add(entity);

    public void Update()
    {
        foreach (var entity in pendingRemove)
        {
            current.Remove(entity);
            entity.Removed();
            entity.Scene = null;
        }
        foreach (var entity in pendingAdd)
        {
            current.Add(entity);
            pendingAwake.Add(entity);
            entity.Scene = Scene;
            entity.Added();
        }
        foreach (var entity in pendingAwake)
        {
            entity.Awake();
        }
        pendingAwake.Clear();
        pendingRemove.Clear();
        pendingAdd.Clear();
        foreach (var entity in current)
        {
            entity.Update();
        }
    }

    public void UpdateDeferred()
    {
        foreach(var entity in current)
        {
            entity.UpdateDeferred();
        }
    }

    public void Draw()
    {
        foreach (var entity in current)
        {
            //TODO: depth
            entity.Draw();
        }
    }

    public int Count
        => current.Count;

    IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator()
        => current.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => current.GetEnumerator();
}