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

    public abstract bool CollideCheck(BoxCollider other);
    public abstract bool CollideCheck(CircleCollider other);
    public abstract bool CollideCheck(Vector2 with);
    public abstract RectangleF GetRelativeBound();
}