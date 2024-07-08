using Dagon.WinAPI;

namespace Dagon.Windows;
public class NativeWindow
{
    internal NativeWindow(nint handle)
    {
        Handle = handle;
    }

    public readonly nint Handle;

    public string Title { get => User32.GetWindowTitle(Handle); set => user32.SetWindowText(Handle, value); }
    public Vec4i Rectangle { get => }
    public Vec2i Location { get => }

    public static NativeWindow Create()
    {
        var handle = User32.CreateWindow(0, 32770, string.Empty, 0, 0, 0, 0, 0, 0, 0, ProcessEnviroment.HInstance, 0);
        return FromHandle(handle);
    }

    public static NativeWindow FromHandle(nint handle) => new(handle);
}