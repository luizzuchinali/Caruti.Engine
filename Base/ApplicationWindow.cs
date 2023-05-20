namespace Caruti.Engine.Base;

public class ApplicationWindow : GameWindow
{
    private SKSurface _surface;
    private SkiaCanvas _canvas;
    private readonly GRContext _context;
    public SizeF CurrentSize { get; private set; }

    private Entity[] _entities;

    protected override void OnLoad()
    {
        var baseType = typeof(Entity);
        _entities = baseType.Assembly.GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.IsSubclassOf(baseType) &&
                !type.GetInterfaces().Contains(typeof(Ignore)))
            .Select(Activator.CreateInstance)
            .Where(x => x != null)
            .OfType<Entity>()
            .ToArray();

        Parallel.ForEach(_entities, x => x.Setup());

        base.OnLoad();
    }

    public unsafe ApplicationWindow(GameWindowSettings gameWindowSettings,
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
        CreateCanvas();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        Parallel.ForEach(_entities, x => x.Update(args));
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        SwapBuffers();
        _canvas.Canvas.Clear();

        foreach (var entity in _entities)
            entity.Render(_canvas, args);

        _canvas.Canvas.Flush();
    }

    protected override void Dispose(bool disposing)
    {
        _context.Dispose();
        _canvas.Dispose();
        _surface.Dispose();

        base.Dispose(disposing);
    }
}