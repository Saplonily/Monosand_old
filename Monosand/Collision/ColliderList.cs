using System.Collections;

namespace Monosand;

public sealed class ColliderList : Collider, ICollection<Collider>
{
    private List<Collider> colliders;

    public int Count => colliders.Count;
    public bool IsReadOnly => false;

    public ColliderList(IEnumerable<Collider> colliders)
        => this.colliders = new(colliders);

    public ColliderList(List<Collider> colliders)
        => this.colliders = colliders;

    public override bool CollideCheck(BoxCollider boxCollider)
    {
        foreach (var item in this) 
            if (item.CollideCheck(boxCollider)) 
                return true;
        return false;
    }

    public override bool CollideCheck(CircleCollider circleCollider)
    {
        foreach (var item in this)
            if (item.CollideCheck(circleCollider))
                return true;
        return false;
    }

    public override bool CollideCheck(Vector2 with)
    {
        foreach (var item in this)
            if (item.CollideCheck(with))
                return true;
        return false;
    }

    public override RectangleF GetRelativeBound()
    {
        RectangleF rect = new();
        foreach (var item in this)
            rect = rect.UnitedWith(item.GetRelativeBound());
        return rect;
    }

    public void ReplaceWith(List<Collider> colliders) => this.colliders = colliders;
    public void Add(Collider collider) => colliders.Add(collider);
    public void Clear() => colliders.Clear();
    public bool Contains(Collider item) => colliders.Contains(item);
    public void CopyTo(Collider[] array, int arrayIndex) => colliders.CopyTo(array, arrayIndex);
    public bool Remove(Collider item) => colliders.Remove(item);
    public IEnumerator<Collider> GetEnumerator() => colliders.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => colliders.GetEnumerator();
}