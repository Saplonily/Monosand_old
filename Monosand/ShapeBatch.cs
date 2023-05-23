namespace Monosand;

public sealed class ShapeBatch
{
    private class ShapeItem { public Matrix Matrix = Matrix.Identity; }
    private sealed class RectangleItem : ShapeItem { public VertexPositionColor[] Vertexs; }

    public static readonly short[] RectangleIndexes = { 0, 1, 2, 2, 3, 0 };

    private bool began = false;

    private BasicEffect defaultEffect;
    private List<ShapeItem> batchedItems;
    private readonly GraphicsDevice graphicsDevice;
    private BlendState blendState;
    private SamplerState samplerState;
    private DepthStencilState depthStencilState;
    private RasterizerState rasterizerState;
    private Effect effect;
    private Matrix matrix;

    public ShapeBatch(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
        defaultEffect = new(graphicsDevice)
        {
            World = Matrix.Identity,
            View = Matrix.Identity,
            Projection = Matrix.Identity
        };
        batchedItems = new();
    }

    private void CheckState()
    { 
        if (!began) 
            throw new InvalidOperationException("Begin hasn't called"); 
    }

    public void Begin(
        BlendState blendState = default,
        SamplerState samplerState = default,
        DepthStencilState depthStencilState = default,
        RasterizerState rasterizerState = default,
        Effect effect = default,
        Matrix? matrix = default
        )
    {
        if (began)
            throw new InvalidOperationException("Begin has called!");
        began = true;
        this.blendState = blendState ?? BlendState.AlphaBlend;
        this.samplerState = samplerState ?? SamplerState.PointClamp;
        this.depthStencilState = depthStencilState ?? DepthStencilState.Default;
        this.rasterizerState = rasterizerState ?? RasterizerState.CullCounterClockwise;
        this.matrix = matrix ?? Matrix.Identity;
        this.effect = effect;
    }

    public void DrawLine(
        Vector2 from, Vector2 to,
        Color color,
        float thickness, LineEndStyle lineEndStyle = LineEndStyle.Rectangle
        )
    {
        CheckState();
        if (lineEndStyle is LineEndStyle.Rectangle)
        {
            Vector2 dir = (to - from).SafeNormalized();
            Vector2 pdir = dir.Perpendicular();
            Vector2 leftTop = from + (pdir - dir) * thickness;
            Vector2 leftBottom = from - (pdir + dir) * thickness;
            Vector2 rightBottom = to + (dir - pdir) * thickness;
            Vector2 rightTop = to + (dir + pdir) * thickness;
            DrawRectangle(leftTop, rightTop, rightBottom, leftBottom, color);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public void DrawRectangle(Vector2 leftTop, Vector2 rightBottom, Color color)
    {
        CheckState();
        var vertexs = new VertexPositionColor[4]
        {
            new(new(leftTop, 0f), color),
            new(new(rightBottom.X, leftTop.Y, 0f), color),
            new(new(rightBottom, 0f), color),
            new(new(leftTop.X, rightBottom.Y, 0f), color)
        };
        RectangleItem shapeItem = new();
        shapeItem.Vertexs = vertexs;
        batchedItems.Add(shapeItem);
    }

    public void DrawRectangle(Vector2 leftTop, Vector2 rightTop, Vector2 rightBottom, Vector2 leftBottom, Color color)
    {
        CheckState();
        var vertexs = new VertexPositionColor[4]
        {
            new(new(leftTop, 0f), color),
            new(new(rightTop, 0f), color),
            new(new(rightBottom, 0f), color),
            new(new(leftBottom, 0f), color)
        };
        RectangleItem shapeItem = new();
        shapeItem.Vertexs = vertexs;
        batchedItems.Add(shapeItem);
    }

    public void End()
    {
        if (!began)
            throw new InvalidOperationException("Begin hasn't called!");
        began = false;
        var gd = graphicsDevice;
        gd.BlendState = blendState;
        gd.SamplerStates[0] = samplerState;
        gd.DepthStencilState = depthStencilState;
        gd.RasterizerState = rasterizerState;
        defaultEffect.VertexColorEnabled = true;
        defaultEffect.Projection = Matrix.Identity;
        defaultEffect.World = matrix;
        var vw = graphicsDevice.Viewport.Width;
        var vh = graphicsDevice.Viewport.Height;
        // idk how, but it works
        defaultEffect.View = new Matrix(
            1.0f / vw * 2.0f, 0, 0, 0,
            0, -1.0f / vh * 2.0f, 0, 0,
            0, 0, 1, 0,
            -1, 1, 0, 1
            );

        defaultEffect.CurrentTechnique.Passes[0].Apply();
        if (effect is null)
        {
            foreach (var item in batchedItems)
            {
                if (item is RectangleItem ri)
                {
                    gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, ri.Vertexs, 0, 4, RectangleIndexes, 0, 2);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        else
        {
            throw new NotImplementedException();
        }
        batchedItems.Clear();
    }
}