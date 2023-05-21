namespace Monosand;

// planned colliders:
// BoxCollider
// CircleCollider
// ColliderList
//
// for future
// - RotatedBoxCollider
// - EllipseCollider
// - GridCollider
// - PolygonCollider
public abstract class Collider
{
    private Entity entity;

    public Entity Entity
    {
        get
        {
            if (entity is null)
                throw new InvalidOperationException("Null Entity of Collider.");
            return entity;
        }
        internal set => entity = value;
    }

    public Vector2 EntityPosition => entity.Position;

    public abstract bool CollideCheck(BoxCollider other);
    public abstract bool CollideCheck(CircleCollider other);
    public abstract bool CollideCheck(Vector2 with);
    public abstract RectangleF GetRelativeBound();

    public bool CollideCheck(Collider other) => other switch
    {
        BoxCollider c => CollideCheck(c),
        CircleCollider c => CollideCheck(c),
        ColliderList c => CollideCheck(c),
        _ => throw new NotSupportedException($"Collider type {other.GetType()} not supported.")
    };

    public bool CollideCheck(ColliderList other)
    {
        foreach (var item in other)
            if (item.CollideCheck(this))
                return true;
        return false;
    }

    public RectangleF GetAbsoluteBound()
    {
        var r = GetRelativeBound();
        return r with { Position = r.Position + EntityPosition };
    }

    public bool TryCollideAny<T>(out Entity result) where T : Entity
    {
        foreach (var other in entity.Scene.Tracker.Get<T>())
        {
            if (this.CollideCheck(other.Collider))
            {
                result = other;
                return true;
            }
        }
        result = null;
        return false;
    }

    public bool CollideAny<T>() where T : Entity
        => TryCollideAny<T>(out _);

    public IEnumerable<T> CollideAll<T>() where T : Entity
    {
        List<T> result = new();
        foreach (var other in entity.Scene.Tracker.Get<T>())
        {
            if (this.CollideCheck(other.Collider))
            {
                result.Add(other);
            }
        }
        return result;
    }
}