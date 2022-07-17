namespace GraphicsLibrary.Components;

public class Button : Component
{
    public float Radius { get; set; }
    public string Text { get; set; }
    public Color TextColor { get; set; }
    public float FontSize { get; set; } = 16;
    public Color MouseOverColor { get; set; }

    public override void Render(ICanvas canvas, FrameEventArgs args)
    {
        canvas.FillColor = MouseOver() ? MouseOverColor : BackgroundColor;
        canvas.SetShadow(new SizeF(1, 2), 1, Colors.Black);

        canvas.FillRoundedRectangle(Position.X, Position.Y, Size.Width, Size.Height, Radius);

        canvas.SetShadow(SizeF.Zero, 0, Colors.Black);
        canvas.FontSize = FontSize;
        canvas.FontColor = TextColor;
        canvas.DrawString(Text, Position.X, Position.Y, Size.Width, Size.Height, HorizontalAlignment.Center,
            VerticalAlignment.Center);
    }
}