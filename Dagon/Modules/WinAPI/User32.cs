using System.Drawing;
using System.Reflection.Metadata;

namespace Dagon.WinAPI;
public unsafe static class User32
{
    // Private
    #region Get/Set WindowLong
    static int GetWindowLong(nint handle, WindowLongField field) => user32.GetWindowLong(handle, (int)field);
    static void SetWindowLong(nint handle, WindowLongField field, int value) => user32.SetWindowLong(handle, (int)field, value);
    #endregion

    #region Get/Set WindowLongPtr
    static pointer GetWindowLongPtr(nint handle, WindowLongField field) => (nint)user32.GetWindowLongPtr(handle, (int)field);
    static pointer SetWindowLongPtr(nint handle, WindowLongField field, pointer value) => user32.SetWindowLongPtr(handle, (int)field, value);
    #endregion

    // Public
    #region CreateWindow
    public static pointer CreateWindow(
        WindowExStyles styleEx,
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
        WindowExStyles styleEx,
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
    #endregion

    #region Get/Set WindowTitle
    public static string GetWindowTitle(nint handle)
    {
        const int MAX_LENGTH = 256;

        var buffer = stackalloc char[MAX_LENGTH];
        user32.GetWindowText(handle, buffer, MAX_LENGTH);
        return Marshal.PtrToStringUni((nint)buffer)!;
    }

    public static void SetWindowTitle(nint handle, string text) => user32.SetWindowText(handle, text);
    #endregion

    #region Set WidnowPos
    public static void SetWindowPos(nint handle, SetOrderFlags zOrder, int x, int y, int width, int height, SetWindowPosFlags flags)
        => user32.SetWindowPos(handle, (long)zOrder, x, y, width, height, (uint)flags);

    public static void SetWindowPos(nint handle, int x, int y, int width, int height, SetWindowPosFlags flags)
        => user32.SetWindowPos(handle, handle, x, y, width, height, (uint)flags);

    public static void SetWindowPos(nint handle, int x, int y, int width, int height)
        => SetWindowPos(handle, x, y, width, height, SetWindowPosFlags.NoZOrder);
    #endregion

    #region Get/Set WindowRectangle
    public static Vec4i GetWindowRectangle(nint handle)
    {
        Vec4i rectangle;
        user32.GetWindowRect(handle, &rectangle);
        return rectangle;
    }
    public static void SetWindowRectangle(nint handle, Vec4i rectangle) => SetWindowPos(handle, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    #endregion

    #region Get/Set WindowStyle
    public static WindowStyles GetWindowStyle(nint handle) => (WindowStyles)GetWindowLong(handle, WindowLongField.Style);
    public static void SetWindowStyle(nint handle, WindowStyles style)
    {
        SetWindowLong(handle, WindowLongField.Style, (int)style);
        UpdateWindowStyle(handle);
    }
    #endregion

    #region Get/Set WindowStyle
    public static WindowExStyles GetWindowExStyle(nint handle) => (WindowExStyles)GetWindowLong(handle, WindowLongField.ExStyle);

    public static void SetWindowExStyle(nint handle, WindowExStyles style)
    {
        SetWindowLong(handle, WindowLongField.ExStyle, (int)style);
        UpdateWindowStyle(handle);
    }
    #endregion

    #region Set Window WndProc Function
    public static pointer SetWindowWndProcFunction(nint handle, pointer pointer) => SetWindowLongPtr(handle, WindowLongField.WndProc, pointer);
    #endregion

    #region Call Window WndProc Function
    public static long CallWindowProccess(nint handle, pointer function, WindowMessage message, long wParam, nint lParam) 
        => user32.CallWindowProc(function, handle, (uint)message, (ulong)wParam, lParam);
    #endregion

    #region Update Style
    public static void UpdateWindowStyle(nint handle) 
        => SetWindowPos(handle, 0, 0, 0, 0, SetWindowPosFlags.NoSize | SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder | SetWindowPosFlags.FrameChanged);
    #endregion
}