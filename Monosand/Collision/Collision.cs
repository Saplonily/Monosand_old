namespace Monosand;

public static class Collision
{
    public static bool CheckRectangle(BoxCollider boxA, BoxCollider boxB)
        => boxA.GetAbsoluteBound().Intersects(boxB.GetAbsoluteBound());

    public static bool CheckRectangleAndCircle(BoxCollider box, CircleCollider circle)
    {
        // copied, not expect you to understand.
        var cc = circle.EntityPosition + circle.Position;
        var rh = box.Bound.Size / 2;
        var rc = box.GetAbsoluteBound().Center;
        var d = cc - rc;
        var p = rc + MathS.Clamp(d, -rh, rh);
        d = p - cc;
        return d.LengthSquared() < circle.Radius * circle.Radius;
    }

    public static bool CheckCircle(CircleCollider circleA, CircleCollider circleB)
        => circleA.Radius * circleA.Radius + circleB.Radius * circleB.Radius >=
        Vector2.DistanceSquared(circleA.EntityPosition + circleA.Position, circleB.EntityPosition + circleB.Position);
}