namespace GraphicsLibrary.Components;

public class Button : Component
{
    public float Radius { get; set; }
    public string Text { get; set; }
    public Color TextColor { get; set; }
    public float FontSize { get; set; } = 16;
    public Color MouseOverColor { get; set; }

    private float _timer;

    public override void Render(ICanvas canvas, FrameEventArgs args)
    {
        canvas.FillColor = MouseOver() ? MouseOverColor : BackgroundColor;
        canvas.SetShadow(new SizeF(1, 2), 1, Colors.Black);

        #region Animation Test

        if ((Position.X == Size.X && Position.Y == Size.Y) ||
            (Position.X == 160 && Position.Y == 60))
        {
            _timer = 0;
        }

        if (MouseOver())
        {
            if (_timer < 5)
            {
                Size = Vector2.Lerp(Size, new Vector2(160, 60), _timer / 5);
                _timer += (float)args.Time;
            }
            else
            {
                _timer = 0;
            }
        }
        else
        {
            if (_timer < 5)
            {
                Size = Vector2.Lerp(Size, new Vector2(140, 40), _timer / 5);
            }
            else
            {
                _timer = 0;
            }
        }

        canvas.FillRoundedRectangle(Position.X, Position.Y, Size.X, Size.Y, Radius);

        #endregion

        canvas.SetShadow(SizeF.Zero, 0, Colors.Black);
        canvas.FontSize = FontSize;
        canvas.FontColor = TextColor;
        canvas.DrawString(Text, Position.X, Position.Y, Size.X, Size.Y, HorizontalAlignment.Center,
            VerticalAlignment.Center);
    }
}