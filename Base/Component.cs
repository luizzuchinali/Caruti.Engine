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

    public Vector2 Size { get; set; }
    public Color BackgroundColor { get; set; }

    public abstract void Render(ICanvas canvas, FrameEventArgs args);

    public virtual void Update(ICanvas canvas, FrameEventArgs args)
    {
    }

    protected bool MouseOver()
    {
        var mousePosition = Application.Window.MousePosition;
        return mousePosition.X >= Position.X && mousePosition.X - Position.X <= Size.X &&
               mousePosition.Y >= Position.Y && mousePosition.Y - Position.Y <= Size.Y;
    }

    private Vector2 ApplyAnchor(Vector2 position)
    {
        if (Size.X == 0 || Size.Y == 0)
            return position;

        return Anchor switch
        {
            AnchorPosition.Center => new Vector2(position.X -= Size.X / 2, position.Y -= Size.Y / 2),
            AnchorPosition.LeftCenter => new Vector2(position.X, position.Y -= Size.Y / 2),
            AnchorPosition.RightCenter => new Vector2(position.X -= Size.X, position.Y -= Size.Y / 2),
            AnchorPosition.TopLeft => new Vector2(position.X, position.Y),
            AnchorPosition.TopRight => new Vector2(position.X -= Size.X, position.Y),
            AnchorPosition.BottomLeft => new Vector2(position.X, position.Y -= Size.Y),
            AnchorPosition.BottomRight => new Vector2(position.X -= Size.X, position.Y -= Size.Y),
            AnchorPosition.TopCenter => new Vector2(position.X -= Size.X / 2, position.Y),
            AnchorPosition.BottomCenter => new Vector2(position.X -= Size.X / 2, position.Y -= Size.Y),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}