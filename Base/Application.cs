namespace GraphicsLibrary.Base;

public static class Application
{
    public static ApplicationWindow Window { get; private set; }

    public static void CreateWindow<T>(NativeWindowSettings settings) where T : MainContent, new()
    {
        Window = new ApplicationWindow(new T
        {
            Anchor = AnchorPosition.TopLeft,
            Size = new SizeF(settings.Size.X, settings.Size.Y)
        }, GameWindowSettings.Default, settings);
    }

    public static void Run() => Window.Run();
}