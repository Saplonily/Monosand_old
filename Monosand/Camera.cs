namespace Monosand;

public class Camera
{
    protected bool dirty;
    protected Matrix matrix;
    protected Matrix invertedMatrix;

    protected Vector2 scale;
    protected Vector2 translation;
    protected float rotation;
    protected Vector2 size;
    protected Vector2 origin; /* 0 ~ 1 */

    public Vector2 Position { get => Translation; set => Translation = value; }
    public virtual Vector2 Translation { get => translation; set { translation = value; dirty = true; } }
    public virtual float Rotation { get => rotation; set { rotation = value; dirty = true; } }
    public virtual Vector2 Origin { get => origin; set { origin = value; dirty = true; } }
    public Vector2 Center => Position + Size / 2;
    public virtual Vector2 Size
    {
        get => size;
        set
        {
            if (value is { X: 0 } or { Y: 0 })
                throw new ArgumentException("Size cannot be 0.", nameof(value));
            size = value;
            dirty = true;
        }
    }
    public virtual Vector2 Scale
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
        dirty = true;
        this.size = size;
        origin = new(0.5f);
        scale = Vector2.One;
        translation = default;
        rotation = 0f;
    }

    protected virtual void UpdateMatrix()
    {
        Vector2 originPos = size * origin;
        matrix =
            Matrix.CreateTranslation(-originPos.X, -originPos.Y, 0f) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateTranslation(originPos.X, originPos.Y, 0f) *
            Matrix.CreateScale(scale.X, scale.Y, 1f) *
            Matrix.CreateTranslation(translation.X, translation.Y, 0f);
        invertedMatrix = Matrix.Invert(matrix);
        dirty = false;
    }

    public Matrix Matrix { get { if (!dirty) return matrix; UpdateMatrix(); return matrix; } }

    public Matrix InvertedMatrix { get { if (!dirty) return invertedMatrix; UpdateMatrix(); return invertedMatrix; } }
}