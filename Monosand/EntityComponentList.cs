using System.Collections;
using System.ComponentModel;

namespace Monosand;

public class EntityComponentList : IReadOnlyCollection<Component>
{
    public Entity Entity { get; }
    private readonly List<Component> current;
    private readonly List<Component> pendingAdd;
    private readonly List<Component> pendingRemove;

    public EntityComponentList(Entity belongTo)
    {
        Entity = belongTo;
        current = new(8);
        pendingAdd = new(4);
        pendingRemove = new(2);
    }

    public void Add(Component component)
        => pendingAdd.Add(component);

    public void Remove(Component component)
        => pendingRemove.Add(component);

    public void Update()
    {
        foreach (var component in pendingRemove)
        {
            current.Remove(component);
            component.Removed();
            component.Entity = null;
        }
        foreach (var component in pendingAdd)
        {
            current.Add(component);
            component.Entity = Entity;
            component.Added();
        }
        pendingRemove.Clear();
        pendingAdd.Clear();
        foreach (var component in current)
        {
            component.Update();
        }
    }

    public void UpdateDeferred()
    {
        foreach (var component in current)
        {
            component.UpdateDeferred();
        }
    }

    public void Draw()
    {
        foreach (var component in current)
        {
            if (component.Visible)
            {
                //TODO: depth
                component.Draw();
            }
        }
    }

    public int Count
        => current.Count;

    IEnumerator<Component> IEnumerable<Component>.GetEnumerator()
        => current.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => current.GetEnumerator();
}