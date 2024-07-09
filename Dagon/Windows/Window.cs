namespace Dagon;
public class Window : IDisposable
{
    public Window(string? title = null, Vec2i? location = null, Vec2i? size = null, bool visible = true)
    {
        var style = (WindowStyles)0;
        var exStyle = (WindowExStyles)0;

        if (visible)
            style |= WindowStyles.VISIBLE;

        NativeWindow = NativeWindow.Create(title, location, size, style, exStyle);
    }

    public readonly NativeWindow NativeWindow;

    public string Title { get => NativeWindow.Title; set => NativeWindow.Title = value; }

    public Vec4i Rectangle { get => NativeWindow.Rectangle; set => NativeWindow.Rectangle = value; }
    public Vec2i Location { get => NativeWindow.Location; set => NativeWindow.Location = value; }
    public Vec2i Size { get => NativeWindow.Size; set => NativeWindow.Size = value; }

    public void StartMessageLoop() => NativeWindow.StartMessageLoop();

    public void Dispose()
    {
        NativeWindow.Dispose();
    }
    ~Window() => Dispose();
}