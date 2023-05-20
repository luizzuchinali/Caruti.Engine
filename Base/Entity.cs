namespace Caruti.Engine.Base;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public SizeF Size { get; set; } = new SizeF(0, 0);

    public virtual void Setup()
    {
    }

    public abstract void Update(FrameEventArgs args);
    public abstract void Render(ICanvas canvas, FrameEventArgs args);
}