namespace Dagon;
public class Window : IDisposable
{
    #region Init
    public Window(
        string? title = null,
        Vec2i? location = null,
        Vec2i? size = null,
        bool visible = true,
        bool caption = false,
        bool closeButton = false,
        bool minimazeButton = false,
        bool maximizeButton = false)
    {
        WindowStyles style = default;
        WindowExStyles exStyle = default;

        if (visible)
            style |= WindowStyles.Visible;

        if (caption)
        {
            style |= WindowStyles.Caption;
            if (closeButton)
            {
                style |= WindowStyles.SystemMenu;

                if (minimazeButton)
                    style |= WindowStyles.MinimizeBox;

                if (maximizeButton)
                    style |= WindowStyles.MaximizeBox;
            }
        }

        style |= WindowStyles.MinimizeBox;

        NativeWindow = NativeWindow.Create(title, location, size, style, exStyle);
        NativeWindow.OnWindowMessage.Register(this, OnWindowMessage);
    }

    public readonly NativeWindow NativeWindow;

    public void StartMessageLoop() => NativeWindow.StartMessageLoop();
    #endregion

    #region Properties
    public string Title { get => NativeWindow.Title; set => NativeWindow.Title = value; }

    public Vec4i Rectangle { get => NativeWindow.Rectangle; set => NativeWindow.Rectangle = value; }
    public Vec2i Location { get => NativeWindow.Location; set => NativeWindow.Location = value; }
    public Vec2i Size { get => NativeWindow.Size; set => NativeWindow.Size = value; }
    #endregion

    #region Events
    protected private void OnWindowMessage(WindowDispatcher.WndProcEvent args)
    {
        var (message, wParam, lParam) = (args.Message, args.WParam, args.LParam);

        switch (message)
        {
            case WindowMessage
        }
    }
    #endregion

    #region Dispose
    public void Dispose()
    {
        NativeWindow.Dispose();
    }
    ~Window() => Dispose();
    #endregion
}