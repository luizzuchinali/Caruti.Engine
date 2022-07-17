namespace GraphicsLibrary;

public class AppContent : MainContent
{
    public override void Render(ICanvas canvas, FrameEventArgs args)
    {
        canvas.FillColor = Color.FromRgb(18, 80, 135);
        canvas.FillRectangle(Position.X, Position.Y, Size.Width, Size.Height);
        Children = new List<IComponent>
        {
            new Button
            {
                Anchor = AnchorPosition.Center,
                Text = "Clique aqui :)",
                Size = new SizeF(140, 40),
                Position = new Vector2(Size.Width / 2, Size.Height / 2),
                MouseOverColor = Colors.Aqua,
                Radius = 20
            }
        };
        base.Render(canvas, args);
    }
}