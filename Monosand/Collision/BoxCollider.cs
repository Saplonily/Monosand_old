namespace Monosand;

public sealed class BoxCollider : Collider
{
    public RectangleF Bound;

    public BoxCollider(RectangleF bound)
        => Bound = bound;

    public BoxCollider(Vector2 size)
        => Bound = new(Vector2.Zero, size);

    public BoxCollider(float size)
        => Bound = new(Vector2.Zero, new Vector2(size));

    public override bool CollideCheck(BoxCollider other)
        => Collision.CheckRectangle(this, other);

    public override bool CollideCheck(CircleCollider other)
        => Collision.CheckRectangleAndCircle(this, other);

    public override bool CollideCheck(Vector2 with)
        => Bound.Contains(with);

    public override RectangleF GetRelativeBound()
        => Bound;
}