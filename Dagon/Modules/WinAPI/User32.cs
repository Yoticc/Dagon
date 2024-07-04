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
    {
        var result = user32.CreateWindowEx((int)styleEx, className, windowName, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);
        return result;
    }

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
    {
        var result = user32.CreateWindowEx((int)styleEx, (char*)className, windowName, (int)style, x, y, width, height, hWndParent, menu, hInstance, lpParam);
        return result;
    }
}