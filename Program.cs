const int width = 800;
const int height = 600;

var settings = new NativeWindowSettings()
{
    Size = new Vector2i(width, height),
    Title = "Cross graphics .NET",
    API = ContextAPI.OpenGL,
    APIVersion = new Version(4, 6),
    IsEventDriven = false,
};

Application.CreateWindow<AppContent>(settings);
Application.Run();