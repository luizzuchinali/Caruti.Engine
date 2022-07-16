namespace GraphicsLibrary;

[Obsolete("Obsolete")]
public class ApplicationWindow : GameWindow
{
    private SKSurface _surface;
    private SkiaCanvas _canvas;
    private GRContext _context;
    public SizeF CurrentSize { get; private set; }

    public unsafe ApplicationWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
        base(
            gameWindowSettings, nativeWindowSettings)
    {
        CurrentSize = new SizeF(Size.X, Size.Y);
        Console.WriteLine($"OpenGL version: {GL.GetString(StringName.Version)}");
        Console.WriteLine($"Vendor: {GL.GetString(StringName.Vendor)}");
        Console.WriteLine($"Renderer: {GL.GetString(StringName.Renderer)}");

        var glInterface =
            GRGlInterface.AssembleGlInterface(GLFW.GetWGLContext(WindowPtr), (_, name) => GLFW.GetProcAddress(name));
        _context = GRContext.CreateGl(glInterface);
        CreateCanvas();

        Button = new Button
        {
            Text = "Click Here :)",
            Position = new Vector2(CurrentSize.Width / 2, CurrentSize.Height / 2),
            Size = new Vector2(140, 40),
            Radius = 20,
            BackgroundColor = Color.FromRgb(183, 193, 201),
            MouseOverColor = Colors.Red,
            TextColor = Color.FromRgb(14, 15, 15)
        };
    }

    private void CreateCanvas()
    {
        var backendRenderTargetDescription = new GRBackendRenderTargetDesc
        {
            Config = GRPixelConfig.Rgba8888, Width = (int)CurrentSize.Width, Height = (int)CurrentSize.Height,
            Origin = GRSurfaceOrigin.BottomLeft,
            RenderTargetHandle = new IntPtr(0), SampleCount = 0, StencilBits = 8
        };

        _surface = SKSurface.Create(_context, backendRenderTargetDescription);
        _canvas = new SkiaCanvas { Canvas = _surface.Canvas };
    }

    public Button Button { get; set; }

    protected override void OnResize(ResizeEventArgs e)
    {
        CurrentSize = new SizeF(e.Width, e.Height);
        CreateCanvas();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        _canvas.FillColor = Color.FromRgb(18, 80, 135);
        _canvas.FillRectangle(0, 0, CurrentSize.Width, CurrentSize.Height);


        Button.Render(_canvas, args);

        _canvas.Canvas.Flush();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        SwapBuffers();
        Button.Update(_canvas, args);
    }

    protected override void Dispose(bool disposing)
    {
        _context.Dispose();
        _canvas.Dispose();
        _surface.Dispose();

        base.Dispose(disposing);
    }
}