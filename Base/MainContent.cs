namespace GraphicsLibrary.Base;

public abstract class MainContent : Component
{
    protected MainContent()
    {
        Resize = parentSize => Size = parentSize;
    }
}