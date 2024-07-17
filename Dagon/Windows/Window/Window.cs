using Dagon.OpenGL;
using System.Diagnostics;

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
        bool maximizeButton = false,
        bool allowPaint = true)
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

        if (allowPaint)
            style |= WindowStyles.OverlappedWindow;


        NativeWindow = NativeWindow.Create(title, location, size, style, exStyle);
        RegisterEvents();
        UpdateProperties();
    }

    public readonly NativeWindow NativeWindow;

    public void StartMessageLoop(WindowWorkspace workspace) => NativeWindow.StartMessageLoop(workspace);
    #endregion

    #region Properties
    public string Title { get => NativeWindow.Title; set => NativeWindow.Title = value; }


    Vec4i bufferedRectangle;
    Vec2i bufferedLocation;
    Vec2i bufferedSize;
    public Vec4i Rectangle { get => bufferedRectangle; set => NativeWindow.Rectangle = value; }
    public Vec2i Location { get => bufferedLocation; set => NativeWindow.Location = value; }
    public Vec2i Size { get => bufferedSize; set => NativeWindow.Size = value; }

    protected private void UpdateProperties()
    {
        bufferedRectangle = NativeWindow.Rectangle;
        bufferedLocation = NativeWindow.Location;
        bufferedSize = NativeWindow.Size;
    }

    public WindowEvents Events = new();
    #endregion

    #region Events
    void RegisterEvents()
    {
        NativeWindow.OnWindowMessage.Register(this, OnWindowMessage);

        Events.Move.Register(this, args => OnMove(args.X, args.Y));
        Events.Resize.Register(this, args => OnResize(args.Width, args.Height));
        Events.Close.Register(this, OnClose);
    }

    protected private virtual void OnWindowMessage(WindowDispatcher.WndProcEvent args)
    {
        var (message, wParam, lParam) = (args.Message, args.WParam, args.LParam);

        var canceled = false;
        switch (message)
        {
            case WindowMessage.Size:
                {
                    canceled = Events.Resize.Invoke(new() { Width = WLeft(), Height = WRight() });
                    break;
                }
            case WindowMessage.Move:
                {
                    canceled = Events.Move.Invoke(new() { X = WLeft(), Y = WRight() });
                    break;
                }
            case WindowMessage.Close:
            case WindowMessage.Destroy:
                {
                    canceled = Events.Close.Invoke(new());
                    break;
                }
        }

        int WLeft() => (int)(wParam & 0xFFFF);
        int WRight() => (int)((wParam >> 16) & 0xFFFF);

        if (canceled)
            args.SetCanceled(true);
    }

    protected private virtual void OnMove(int x, int y)
    {
        bufferedLocation = new(x, y);
        bufferedRectangle = new(x, y, bufferedSize.X + x, bufferedSize.Y + y);
    }

    protected private virtual void OnResize(int width, int height)
    {
        bufferedSize = new(width, height);
        var bufferedLocation = this.bufferedLocation;
        bufferedRectangle = new(bufferedLocation.X, bufferedLocation.Y, bufferedLocation.X + width, bufferedLocation.Y + height);
    }

    protected private virtual void OnClose(Event @event) 
    {
        user32.CloseWindow(NativeWindow.Handle);
        Process.GetCurrentProcess().Kill();
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