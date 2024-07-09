namespace Dagon;
public unsafe class WindowDispatcher : IDisposable
{
    public WindowDispatcher(nint handle)
    {
        Handle = handle;
        PreviousFunction = User32.SetWindowWndProcFunction(Handle, ldftn(WndProc));
        Instances.Add(Handle, this);
    }

    public nint Handle;
    public pointer PreviousFunction;
    public EventBus<WndProcEvent> OnWindowMessage = new();

    public void Dispose()
    {
        Instances.Remove(Handle);
        User32.SetWindowWndProcFunction(Handle, PreviousFunction);
    }
    ~WindowDispatcher() => Dispose();

    static Dictionary<nint, WindowDispatcher> Instances = [];
    public static long WndProc(nint hWnd, WindowMessage message, long wParam, nint lParam)
    {
        var instance = Instances[hWnd];

        if (instance.OnWindowMessage.Invoke(new(hWnd, message, wParam, lParam)))
            return User32.CallWindowProccess(instance.Handle, instance.PreviousFunction, message, wParam, lParam);
        else return 0;
    }

    public class WndProcEvent : Event
    {
        public WndProcEvent(nint hWnd, WindowMessage message, long wParam, long lParam)
        {
            HWnd = hWnd;
            Message = message;
            WParam = wParam;
            LParam = lParam;
        }

        public nint HWnd;
        public WindowMessage Message;
        public long WParam;
        public long LParam;
    }
}