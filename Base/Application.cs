namespace Caruti.Engine.Base;

public static class Application
{
    public static ApplicationWindow? Window { get; private set; }

    public static void CreateWindow(NativeWindowSettings settings)
    {
        Window = new ApplicationWindow(GameWindowSettings.Default, settings);
    }

    public static void Run()
    {
        if (Window is null)
            throw new NullReferenceException("Window isn't created");

        Window?.Run();
    }
}