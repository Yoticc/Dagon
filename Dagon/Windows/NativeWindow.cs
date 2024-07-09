namespace Dagon;
public unsafe class NativeWindow : IDisposable
{
    NativeWindow(nint handle, WindowDispatcher dispatcher)
    {
        Handle = handle;
        Dispatcher = dispatcher;
        OnWindowMessage = Dispatcher.OnWindowMessage;
    }

    public readonly nint Handle;
    public readonly WindowDispatcher Dispatcher;
    public readonly EventBus<WindowDispatcher.WndProcEvent> OnWindowMessage;

    public string Title { get => User32.GetWindowTitle(Handle); set => User32.SetWindowTitle(Handle, value); }

    public Vec4i Rectangle { get => User32.GetWindowRectangle(Handle); set => User32.SetWindowRectangle(Handle, value); }
    public Vec2i Location { get => Rectangle.LowerVec2i; set => Rectangle.ex(rect => User32.SetWindowRectangle(Handle, new(value.X, value.Y, value.X + rect.Width, value.Y + rect.Height))); }
    public Vec2i Size { get => Rectangle.SizeVec2i; set => Rectangle.ex(rect => User32.SetWindowRectangle(Handle, new(rect.X, rect.Y, rect.W + value.X, rect.Z + value.Y))); }

    public WindowStyles Style { get => User32.GetWindowStyle(Handle); set => User32.SetWindowStyle(Handle, value); }
    public WindowExStyles ExStyle { get => User32.GetWindowExStyle(Handle); set => User32.SetWindowExStyle(Handle, value); }

    public void StartMessageLoop()
    {
        nint message;
        while (user32.GetMessage(&message, Handle, 0, 0))
        {
            user32.TranslateMessage(&message);
            user32.DispatchMessage(&message);
        }
    }

    public void Dispose()
    {
        Dispatcher.Dispose();
        user32.DestroyWindow(Handle);
    }
    ~NativeWindow() => Dispose();

    public static NativeWindow Create(string? title = null, Vec2i? location = null, Vec2i? size = null, WindowStyles? style = null, WindowExStyles? exStyles = null)
    {
        var handle = User32.CreateWindow(0, 32770, string.Empty, 0, 0, 0, 0, 0, 0, 0, ProcessEnviroment.HInstance, 0);
        var window = FromHandle(handle);

        #region Title
        if (title is not null)
            window.Title = title;
        #endregion

        #region Location and Size
        if (location is not null || size is not null)
        {
            var rect = new Vec4i();

            if (location is not null)
            {
                rect.X = location.Value.X;
                rect.Y = location.Value.Y;
            }
            else
            {
                var currentRect = window.Rectangle;
                rect.X = currentRect.X;
                rect.Y = currentRect.Y;
            }

            if (size is not null)
            {
                rect.Width = size.Value.X;
                rect.Height = size.Value.Y;
            }
            else
            {
                var currentRect = window.Rectangle;
                rect.Width = currentRect.Width;
                rect.Width = currentRect.Width;
            }

            window.Rectangle = rect;
        }
        #endregion

        #region Style and ExStyle
        window.Style = style??default;
        window.ExStyle = exStyles??default;
        #endregion

        return window;
    }

    public static NativeWindow FromHandle(nint handle)
    {
        var dispatcher = new WindowDispatcher(handle);
        return new(handle, dispatcher);
    }
}