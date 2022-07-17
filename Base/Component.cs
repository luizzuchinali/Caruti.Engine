namespace GraphicsLibrary.Base;

public abstract class Component : IComponent
{
    public AnchorPosition Anchor { get; set; } = AnchorPosition.Center;

    private Vector2 _position;

    public Vector2 Position
    {
        get => ApplyAnchor(_position);
        set => _position = value;
    }

    public SizeF Size { get; set; }
    public Color BackgroundColor { get; set; }

    public Action<SizeF> Resize { get; protected set; }

    public IList<IComponent> Children { get; set; } = new List<IComponent>();

    public virtual void Render(ICanvas canvas, FrameEventArgs args)
    {
        foreach (var child in Children)
            child.Render(canvas, args);
    }

    public virtual void Update(ICanvas canvas, FrameEventArgs args)
    {
        foreach (var child in Children)
            child.Update(canvas, args);
    }

    protected bool MouseOver()
    {
        var mousePosition = Application.Window.MousePosition;
        return mousePosition.X >= Position.X && mousePosition.X - Position.X <= Size.Width &&
               mousePosition.Y >= Position.Y && mousePosition.Y - Position.Y <= Size.Height;
    }

    private Vector2 ApplyAnchor(Vector2 position)
    {
        if (Size.Width == 0 || Size.Height == 0)
            return position;

        return Anchor switch
        {
            AnchorPosition.Center => new Vector2(position.X -= Size.Width / 2, position.Y -= Size.Height / 2),
            AnchorPosition.LeftCenter => new Vector2(position.X, position.Y -= Size.Height / 2),
            AnchorPosition.RightCenter => new Vector2(position.X -= Size.Width, position.Y -= Size.Height / 2),
            AnchorPosition.TopLeft => new Vector2(position.X, position.Y),
            AnchorPosition.TopRight => new Vector2(position.X -= Size.Width, position.Y),
            AnchorPosition.BottomLeft => new Vector2(position.X, position.Y -= Size.Height),
            AnchorPosition.BottomRight => new Vector2(position.X -= Size.Width, position.Y -= Size.Height),
            AnchorPosition.TopCenter => new Vector2(position.X -= Size.Width / 2, position.Y),
            AnchorPosition.BottomCenter => new Vector2(position.X -= Size.Width / 2, position.Y -= Size.Height),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}