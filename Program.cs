const int width = 800;
const int height = 600;

var settings = new NativeWindowSettings()
{
    Size = new Vector2i(width, height),
    Title = "Cross graphics .NET",
    API = ContextAPI.OpenGL,
    APIVersion = new Version(4, 6),
    IsEventDriven = false,
    Location = new Vector2i(1550, 300),
    Vsync = VSyncMode.On,
};

Application.CreateWindow(settings);
Application.Run();