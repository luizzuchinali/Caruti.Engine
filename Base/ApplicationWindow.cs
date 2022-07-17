namespace GraphicsLibrary.Base;

[Obsolete("Obsolete")]
public class ApplicationWindow : GameWindow
{
    private SKSurface _surface;
    private SkiaCanvas _canvas;
    private GRContext _context;
    public SizeF CurrentSize { get; private set; }

    public MainContent MainContent { get; }

    public unsafe ApplicationWindow(MainContent mainContent, GameWindowSettings gameWindowSettings,
        NativeWindowSettings nativeWindowSettings) :
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
        MainContent = mainContent;
        CreateCanvas();
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

    protected override void OnResize(ResizeEventArgs e)
    {
        CurrentSize = new SizeF(e.Width, e.Height);
        MainContent.Resize?.Invoke(CurrentSize);
        CreateCanvas();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        MainContent.Render(_canvas, args);
        _canvas.Canvas.Flush();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        SwapBuffers();
        MainContent.Update(_canvas, args);
    }

    protected override void Dispose(bool disposing)
    {
        _context.Dispose();
        _canvas.Dispose();
        _surface.Dispose();

        base.Dispose(disposing);
    }
}