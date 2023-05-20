namespace Caruti.Engine;

public class BouncingBall : Entity
{
    private Vector2 _offset;

    public BouncingBall()
    {
        Position = new Vector2(Application.Window!.CurrentSize.Width / 2, Application.Window!.CurrentSize.Height / 2);
        Size = new SizeF(16, 16);
        _offset = new Vector2(-1, 1);
    }

    public override void Update(FrameEventArgs args)
    {
        const int velocity = 500;
        var box = Application.Window!.CurrentSize;
        
        if (Position.X >= box.Width)
            _offset.X = -1;
        else if (Position.X <= 0)
            _offset.X = +1;

        if (Position.Y >= box.Height)
            _offset.Y = -1;
        else if (Position.Y <= 0)
            _offset.Y = 1;

        Position += _offset * velocity * (float)args.Time;
    }

    public override void Render(ICanvas canvas, FrameEventArgs args)
    {
        canvas.FillColor = Colors.Red;
        canvas.StrokeColor = Colors.Red;
        canvas.FillEllipse(Position.X, Position.Y, Size.Width, Size.Height);
        canvas.DrawEllipse(Position.X, Position.Y, Size.Width, Size.Height);
    }
}