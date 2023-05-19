namespace Monosand;

public class Camera
{
    protected bool dirty;
    protected Matrix matrix;
    protected Matrix invertedMatrix;

    protected RectangleF bound;
    protected Vector2 scale;
    protected float rotation;
    protected Vector2 origin;

    public RectangleF Bound => bound;
    public Vector2 Position { get => bound.Position; set { bound.Position = value; dirty = true; } }
    public float Rotation { get => rotation; set { rotation = value; dirty = true; } }
    public Vector2 Origin { get => origin; set { origin = value; dirty = true; } }
    public Vector2 Size
    {
        get => bound.Size;
        set
        {
            if (value is { X: 0 } or { Y: 0 })
                throw new ArgumentException("Size cannot be 0.", nameof(value));
            bound.Size = value;
            dirty = true;
        }
    }
    public Vector2 Scale
    {
        get => scale;
        set
        {
            if (value is { X: 0 } or { Y: 0 })
                throw new ArgumentException("Scale cannot be 0.", nameof(value));
            scale = value;
            dirty = true;
        }
    }

    public Camera(Vector2 size)
    {
        bound.Position = default;
        bound.Size = size;
        origin = bound.Size / 2;
        scale = Vector2.One;
        rotation = 0f;
        dirty = true;
    }

    protected virtual void UpdateMatrix()
    {
        matrix =
            Matrix.CreateTranslation(-origin.X, -origin.Y, 0f) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateTranslation(origin.X, origin.Y, 0f) *
            Matrix.CreateScale(scale.X, scale.Y, 1f) *
            Matrix.CreateTranslation(bound.Position.X, bound.Position.Y, 0f);
        invertedMatrix = Matrix.Invert(matrix);
        dirty = false;
    }

    public Matrix Matrix { get { if (!dirty) return matrix; UpdateMatrix(); return matrix; } }

    public Matrix InvertedMatrix { get { if (!dirty) return invertedMatrix; UpdateMatrix(); return invertedMatrix; } }
}