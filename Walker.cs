namespace Caruti.Engine;

public class Walker : Entity, Ignore
{
    private readonly IList<(Vector2, Color)> _steps = new List<(Vector2, Color)>();

    private Vector2 _lastStepPos;

    public Walker()
    {
        Position = new Vector2(Application.Window!.CurrentSize!.Width / 2, Application.Window!.CurrentSize.Height / 2);
        _lastStepPos = Position;
    }

    public override void Update(FrameEventArgs args)
    {
        var random = new Random();
        var stepX = (float)Math.Floor(random.NextDouble() * 3) - 1;
        var stepY = (float)Math.Floor(random.NextDouble() * 3) - 1;

        var stepPos = _lastStepPos + new Vector2(stepX, stepY);

        var generated = (int)Math.Floor(random.NextDouble() * 3) - 1;
        var color = generated switch
        {
            -1 => Colors.Red,
            0 => Colors.Blue,
            _ => Colors.Green
        };

        _steps.Add((stepPos, color));
        _lastStepPos = stepPos;
    }

    public override void Render(ICanvas canvas, FrameEventArgs args)
    {
        canvas.StrokeColor = Colors.White;
        canvas.FillColor = Colors.White;

        // if (Application.Window!.MouseState.IsButtonDown(MouseButton.Left))
        // {
        //     var mousePos = Application.Window.MouseState.Position;
        //     canvas.FillRectangle(mousePos.X - 50 / 2, mousePos.Y - 50 / 2, 50, 50);
        // }
        
        foreach (var (vec, color) in _steps)
        {
            // canvas.StrokeColor = color;
            // canvas.FillColor = color;
            canvas.FillRectangle(vec.X, vec.Y, 1, 1);
        }
    }
}