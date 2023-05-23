namespace Monosand;

public static class MathS
{
    public const float Sqrt2 = 1.41421356237309504880168872420969f;

    public static float Approach(float value, float target, float maxMove)
        => value <= target ? Math.Min(value + maxMove, target) : Math.Max(value - maxMove, target);

    public static Vector2 Approach(Vector2 value, Vector2 target, float maxMove)
    {
        Vector2 move = target - value;
        return move.Length() <= maxMove ? target : value + Vector2.Normalize(move) * maxMove;
    }

    public static Vector2 Clamp(Vector2 value, float minX, float minY, float maxX, float maxY)
        => new(Math.Clamp(value.X, minX, maxX), Math.Clamp(value.Y, minY, maxY));

    public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        => Clamp(value, min.X, min.Y, max.X, max.Y);

    public static Vector2 Normalized(this Vector2 value)
        => Vector2.Normalize(value);

    public static Vector2 SafeNormalized(this Vector2 value)
    {
        float l = value.LengthSquared();
        return l != 0 ? value / MathF.Sqrt(l) : Vector2.Zero;
    }

    public static Vector2 SafeNormalized(this Vector2 value, Vector2 whenZero)
    {
        float l = value.LengthSquared();
        return l != 0 ? value / MathF.Sqrt(l) : whenZero;
    }

    public static Vector2 Perpendicular(this Vector2 vector)
        => new(-vector.Y, vector.X);

    #region RandomMath

    public static double NextDouble(this Random r, double min, double max)
        => (r.NextDouble() * (max - min)) + min;

    public static double NextDouble(this Random r, double max)
        => r.NextDouble(0f, max);

    public static double Next1m1Double(this Random r, double num)
        => r.NextDouble(-num, num);

    public static float NextFloat(this Random r)
        => (float)r.NextDouble();

    public static float Next1m1Float(this Random r, float num)
        => r.NextFloat(-num, num);

    public static float NextFloat(this Random r, float min, float max)
        => (r.NextFloat() * (max - min)) + min;

    public static float NextFloat(this Random r, float max)
        => r.NextFloat(0f, max);

    public static int Next1m1(this Random r)
        => r.Next(2) * 2 - 1;

    public static float Next1m1Float(this Random r, float min, float max)
        => r.NextFloat(min, max) * r.Next1m1();

    public static Vector2 NextVector2(this Random r, Vector2 min, Vector2 max)
        => new(r.NextFloat(min.X, max.X), r.NextFloat(min.Y, max.Y));

    public static Vector2 NextVector2(this Random r, Vector2 max)
        => new(r.NextFloat(max.X), r.NextFloat(max.Y));

    #endregion
}