using OpenGL;
using System.Drawing;
using static Dagon.WinAPI.PixelFormatDescriptor;

namespace Dagon.OpenGL;
public unsafe class WindowWorkspace : IDisposable
{
    public WindowWorkspace(Window window)
    {
        Window = window;
        Context = new(window.NativeWindow.HDC);
    }

    public readonly Window Window;
    public readonly GLContext Context;

    public void Dispose()
    {
        Context.Dispose();
    }
}