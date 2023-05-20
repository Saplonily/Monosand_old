using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Monosand;

// just union!
[StructLayout(LayoutKind.Explicit)]
public struct RectangleF
{
    [FieldOffset(0)] public Vector2 Position;
    [FieldOffset(8)] public Vector2 Size;
    [FieldOffset(0)] public float Left;
    [FieldOffset(4)] public float Top;
    [FieldOffset(8)] public float Width;
    [FieldOffset(12)] public float Height;
    [FieldOffset(0)] public float X;
    [FieldOffset(4)] public float Y;

    [FieldOffset(0)] public (float, float) TuplePosition;
    [FieldOffset(8)] public (float, float) TupleSize;
    [FieldOffset(0)] public (Vector2, Vector2) Tuple;

    public readonly float Right => Left + Width;
    public readonly float Bottom => Top + Height;
    public readonly Vector2 Center => Position + Size / 2;
    public readonly Vector2 LeftTop => Position;
    public readonly Vector2 RightTop => Position + new Vector2(Width, 0f);
    public readonly Vector2 LeftBottom => Position + new Vector2(0f, Height);
    public readonly Vector2 RightBottom => Position + new Vector2(Width, Height);
    public readonly Vector2 TopCenter => Position + new Vector2(Width / 2, 0f);
    public readonly Vector2 LeftCenter => Position + new Vector2(0f, Height / 2);
    public readonly Vector2 RightCenter => Position + new Vector2(Width, Height / 2);
    public readonly Vector2 BottomCenter => Position + new Vector2(Width / 2, Height);
    public readonly float Area => Width * Height;

    public RectangleF() { }

    public RectangleF(Vector2 position, Vector2 size)
        => (Position, Size) = (position, size);

    public RectangleF(float x, float y, float width, float height)
        => (X, Y, Width, Height) = (x, y, width, height);

    public RectangleF(float x, float y, Vector2 size)
        => (X, Y, Size) = (x, y, size);

    public RectangleF(Vector2 position, float width, float height)
        => (Position, Width, Height) = (position, width, height);

    public readonly override bool Equals([NotNullWhen(true)] object obj)
        => obj is RectangleF r && r == this;

    public readonly override int GetHashCode()
        => HashCode.Combine(Position, Size);

    public readonly override string ToString()
        => $"{{ Position: ({Left},{Top}), Size: ({Width},{Height}) }}";

    public static bool operator ==(RectangleF a, RectangleF b)
        => a.Position == b.Position && a.Size == b.Size;

    public static bool operator !=(RectangleF a, RectangleF b)
        => !(a == b);

    public readonly Rectangle ToRectangle()
        => new((int)X, (int)Y, (int)Width, (int)Height);

    public readonly void Deconstruct(out Vector2 position, out Vector2 size)
        => (position, size) = (Position, Size);

    public readonly void Deconstruct(out float x, out float y, out float width, out float height)
        => (x, y, width, height) = (X, Y, Width, Height);

    public readonly bool Contains(Vector2 point)
        => point.X >= X && point.X < X + Width && point.Y >= Y && point.Y < Y + Height;

    public readonly bool Intersects(RectangleF rect)
        => rect.Left < Right && rect.Right >= Left && rect.Top < Bottom && rect.Bottom >= Top;

    public readonly RectangleF Inflated(float horizontal, float vertical)
        => this with { X = X - horizontal, Y = Y - vertical, Width = Width + 2 * horizontal, Height = Height + 2 * vertical };

    public void Inflate(float horizontal, float vertical)
        => (X, Y, Width, Height) = (X - horizontal, Y - vertical, Width + 2 * horizontal, Height + 2 * vertical);

    public readonly RectangleF ScaleInflated(float horizontalScale, float verticalScale)
        => Inflated(Width * horizontalScale, Height * verticalScale);

    public void ScaleInflate(float horizontalScale, float verticalScale)
        => Inflate(Width * horizontalScale, Height * verticalScale);

    public readonly RectangleF UnitedWith(RectangleF other)
        => new(Math.Min(X, other.X), Math.Min(Y, other.Y), Math.Max(Width, other.Width), Math.Max(Height, other.Height));
}