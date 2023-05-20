namespace Caruti.Engine.Base;

public interface IComponent
{
    public AnchorPosition Anchor { get; set; }
    public Vector2 Position { get; set; }
    public SizeF Size { get; set; }
    public Color BackgroundColor { get; set; }
    public Action<SizeF> Resize { get; }

    public void Render(ICanvas canvas, FrameEventArgs args);
    public void Update(ICanvas canvas, FrameEventArgs args);
}