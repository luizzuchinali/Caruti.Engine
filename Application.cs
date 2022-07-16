namespace GraphicsLibrary;

public class Application
{
    public static ApplicationWindow Window { get; private set; }

    public static void CreateWindow(NativeWindowSettings settings)
    {
        Window = new ApplicationWindow(GameWindowSettings.Default, settings);
    }

    public static void Run() => Window.Run();
}