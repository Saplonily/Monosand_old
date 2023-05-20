namespace Monosand;

public sealed class CircleCollider : Collider
{
    public Vector2 Position;
    public float Radius;

    public CircleCollider(float radius)
        => Radius = radius;

    public CircleCollider(Vector2 position, float radius) : this(radius)
        => Position = position;

    public override bool CollideCheck(BoxCollider boxCollider)
        => Collision.CheckRectangleAndCircle(boxCollider, this);

    public override bool CollideCheck(CircleCollider circleCollider)
        => Collision.CheckCircle(this, circleCollider);

    public override bool CollideCheck(Vector2 with)
        => Vector2.DistanceSquared(Position + EntityPosition, with) <= Radius * Radius;

    public override RectangleF GetRelativeBound()
        => new RectangleF(Position, Vector2.Zero).Inflated(Radius, Radius);
}