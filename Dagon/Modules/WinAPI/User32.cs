namespace Dagon.WinAPI;
public unsafe class User32
{
    public static pointer CreateWindow(
        WindowStylesEx styleEx,
        string className,
        string windowName,
        WindowStyles style,
        int x,
        int y,
        int width,
        int height,
        pointer hWndParent,
        pointer menu,
        pointer hInstance,
        pointer lpParam)
        => user32.CreateWindowEx((int)styleEx, className, windowName, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);

    public static pointer CreateWindow(
        WindowStylesEx styleEx,
        ushort className,
        string windowName,
        WindowStyles style,
        int x, 
        int y,
        int width,
        int height,
        pointer hWndParent,
        pointer menu,
        pointer hInstance,
        pointer lpParam)
        => user32.CreateWindowEx((int)styleEx, (char*)className, windowName, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);

    public static string GetWindowTitle(nint handle)
    {
        const int MAX_LENGTH = 256;

        var buffer = stackalloc char[MAX_LENGTH];
        user32.GetWindowText(handle, buffer, MAX_LENGTH);
        return Marshal.PtrToStringUni((nint)buffer)!;
    }

    public static void SetWindowTitle(nint handle, string text) => user32.SetWindowText(handle, text);

    public static Vec4i GetWindowRectangle(nint handle)
    {
        Vec4i rect;
        user32.GetWindowRect(handle, &rect);
        return rect;
    }
}